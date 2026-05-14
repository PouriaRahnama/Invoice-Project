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
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

            if (invoice == null || invoice.UserId != userId)
                throw new Exception("");
            invoice.Status = status;

            _invoiceRepository.Update(invoice);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    
        public async Task<Guid> CreateAsync(CreateInvoiceDto createInvoiceDto)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            if (createInvoiceDto.Items == null || !createInvoiceDto.Items.Any())
                throw new Exception("");

            var productIds = createInvoiceDto.Items.Select(i => i.ProductId).ToList();

            var products = await _productRepository
                .EntitiesAsNoTracking
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            if (products.Count != productIds.Count)
                throw new InvalidOperationException("One or more products not found.");

            var invoiceNumber = await GenerateInvoiceNumberAsync();


            var invoice = _mapper.Map<Invoice.Domain.Entities.Invoice>(createInvoiceDto);
            invoice.UserId = userId;
            invoice.InvoiceNumber = invoiceNumber;

            long invoiceTotal = 0;

            var items = new List<InvoiceItem>();

            foreach (var itemDto in createInvoiceDto.Items)
            {
                var product = products.First(p => p.Id == itemDto.ProductId);

                // بررسی موجودی
                if (product.Quantity < itemDto.Quantity)
                    throw new Exception($"Not enough stock for product {product.Name}");

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
            }

            invoice.TotalPrice = invoiceTotal;

            await _invoiceRepository.CreateAsync(invoice);
            await _invoiceItemRepository.CreateRangeAsync(items);

            await _unitOfWork.SaveChangesAsync();
            return invoice.Id;
        }
   
        public async Task<GetInvoiceDetailsDto> GetByIdAsync(Guid invoiceId)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            var invoiceDetails = await _invoiceRepository.EntitiesAsNoTracking
                .Where(i => i.Id == invoiceId && i.UserId == userId)
                .ProjectTo<GetInvoiceDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return invoiceDetails ?? new GetInvoiceDetailsDto();
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
