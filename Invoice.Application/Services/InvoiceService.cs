namespace Invoice.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InvoiceService(IInvoiceRepository invoiceRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IInvoiceItemRepository invoiceItemRepository,
            IProductRepository productRepository)
        {
            this._invoiceRepository = invoiceRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            this._invoiceItemRepository = invoiceItemRepository;
            this._productRepository = productRepository;
        }

        public async Task<bool> ChangeStatusAsync(Guid invoiceId, Status status)
        {
            //var userId = _httpContextAccessor.HttpContext.GetUserId();

            //if (userId == null || userId == Guid.Empty)
            //    throw new UnauthorizedException("کاربر شناسایی نشد");

            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

            if (invoice == null)
                throw new NotFoundException("فاکتور مورد نظر یافت نشد.");

            invoice.Status = status;

            _invoiceRepository.Update(invoice);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Guid> CreateAsync(CreateInvoiceDto createInvoiceDto)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            if (userId == null || userId == Guid.Empty)
                throw new UnauthorizedException("کاربر شناسایی نشد");

            if (createInvoiceDto.Items == null || !createInvoiceDto.Items.Any())
                throw new BusinessException("فاکتور باید حداقل یک محصول داشته باشد.");

            var productIds = createInvoiceDto.Items.Select(i => i.ProductId).ToList();

            var products = await _productRepository.Entities
                .Where(p => productIds.Contains(p.Id)).ToListAsync();

            if (products.Count != productIds.Count)
                throw new BusinessException("تعدادی از محصولات وارد شده پیدا نشد.");

            var invoiceNumber = await GenerateInvoiceNumberAsync();

            var invoice = _mapper.Map<Invoice.Domain.Entities.Invoice>(createInvoiceDto);
            invoice.UserId = userId;
            invoice.InvoiceNumber = invoiceNumber;

            long invoiceTotal = 0;

            var items = new List<InvoiceItem>();

            foreach (var itemDto in createInvoiceDto.Items)
            {
                var product = products.First(p => p.Id == itemDto.ProductId);

                if (product.Quantity < itemDto.Quantity)
                    throw new BusinessException($"موجودی محصول کافی نمی باشد. {product.Name}");

                long unitPrice = product.Price;
                long totalBeforeDiscount = unitPrice * itemDto.Quantity;
                long discountAmount = (long)(totalBeforeDiscount * (itemDto.DiscountPercent / 100m));
                long finalItemTotal = totalBeforeDiscount - discountAmount;

                var invoiceItem = new InvoiceItem
                {
                    InvoiceId = invoice.Id,
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = unitPrice,
                    DiscountPercent = itemDto.DiscountPercent,
                    TotalPrice = finalItemTotal
                };

                items.Add(invoiceItem);
                invoiceTotal += finalItemTotal;
                product.Quantity -= itemDto.Quantity;

                // _productRepository.Update(product); tracking => Savechange
            }

            invoice.TotalPrice = invoiceTotal;

            try
            {
                await _invoiceRepository.CreateAsync(invoice);
                await _invoiceItemRepository.CreateRangeAsync(items);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new BusinessException("موجودی یکی از محصولات همزمان تغییر کرده است. لطفا مجدد تلاش کنید.");
            }
            return invoice.Id;
        }

        public async Task<GetInvoiceDetailsDto> GetByIdAsync(Guid invoiceId)
        {
            //var userId = _httpContextAccessor.HttpContext.GetUserId();

            //if (userId == null || userId == Guid.Empty)
            //    throw new UnauthorizedException("کاربر شناسایی نشد");

            var invoiceDetails = await _invoiceRepository.EntitiesAsNoTracking
                .Where(i => i.Id == invoiceId)
                .ProjectTo<GetInvoiceDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (invoiceDetails.Items == null || !invoiceDetails.Items.Any() || invoiceDetails == null)
                throw new NotFoundException("فاکتور مورد نظر یافت نشد.");

            return invoiceDetails;
        }

        public async Task<GetInvoiceDetailsReportDto> GetByIdForReportAsync(Guid invoiceId)
        {
            //var userId = _httpContextAccessor.HttpContext.GetUserId();

            //if (userId == null || userId == Guid.Empty)
            //    throw new UnauthorizedException("کاربر شناسایی نشد");

            var invoiceDetailsReport = await _invoiceRepository.EntitiesAsNoTracking
                .Where(i => i.Id == invoiceId)
                .ProjectTo<GetInvoiceDetailsReportDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (invoiceDetailsReport.Items == null || !invoiceDetailsReport.Items.Any() || invoiceDetailsReport == null)
                throw new NotFoundException("فاکتور مورد نظر یافت نشد.");

            return invoiceDetailsReport;
        }

        public async Task<SearchQueryResponse<GetInvoiceDetailsDto>> GetAllAsync(FilterInvoincesDto QueryParams)
        {
            var mapper = new InvoiceGridifyMapper();
            //var userId = _httpContextAccessor.HttpContext.GetUserId();

            //if (userId == null || userId == Guid.Empty)
            //    throw new UnauthorizedException("کاربر شناسایی نشد");

            var query = _invoiceRepository.EntitiesAsNoTracking
                .ProjectTo<GetInvoiceDetailsDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => EF.Property<DateTime>(x, "CreatedDateTime"))
                .AsQueryable();

            var qp = await query.GridifyQueryableAsync(QueryParams, mapper);

            var pq = new Paging<GetInvoiceDetailsDto>(qp.Count, qp.Query);
            return new SearchQueryResponse<GetInvoiceDetailsDto>(QueryParams, pq);
        }

        private async Task<string> GenerateInvoiceNumberAsync()
        {
            var currentYear = DateTime.UtcNow.Year;
            var latestInvoice = await _invoiceRepository.EntitiesAsNoTracking
                .Where(inv => inv.InvoiceNumber.Contains(currentYear.ToString()))
                .OrderByDescending(inv => inv.InvoiceNumber)
                .FirstOrDefaultAsync();

            string newNumber;
            if (latestInvoice == null)
                newNumber = $"INV-{currentYear}-00001";
            else
            {
                var parts = latestInvoice.InvoiceNumber.Split('-');
                var lastPart = parts.Last();
                int.TryParse(lastPart, out int currentSeq);
                currentSeq++;
                newNumber = $"INV-{currentYear}-{currentSeq:D5}";
            }
            return newNumber;
        }
    }
}
