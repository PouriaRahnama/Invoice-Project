using Invoice.Application.Dtos.InvoiceDtos;

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

            var invoice = _mapper.Map<Invoice.Domain.Entities.Invoice>(createInvoiceDto);

            await _invoiceRepository.CreateAsync(invoice);

            long invoiceTotal = 0;

            var items = new List<InvoiceItem>();

            foreach (var itemDto in createInvoiceDto.Items)
            {
                var product = products.First(p => p.Id == itemDto.ProductId);

                // بررسی موجودی
                if (product.Quantity < itemDto.Quantity)
                    throw new Exception($"Not enough stock for product {product.Name}");

                long unitPrice = product.Price; // قیمت لحظه صدور
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

                // کم کردن از موجودی
                product.Quantity -= itemDto.Quantity;
            }

            invoice.TotalPrice = invoiceTotal;

            await _invoiceItemRepository.CreateRangeAsync(items);

            await _unitOfWork.SaveChangesAsync();
            return invoice.Id;
            //var invoice = new Invoice
            //{
            //    CustomerId = dto.CustomerId,
            //    UserId = userId,
            //    InvoiceNumber = await GenerateInvoiceNumberAsync(),
            //    Status = Status.Pending,
            //    TotalPrice = 0
            //};
        }

        public async Task<InvoiceDetailDto> GetByIdAsync(Guid invoiceId)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            var invoice = await _invoiceRepository.EntitiesAsNoTracking
                .Include(i => i.Customer)
                .Include(i => i.Items)
                    .ThenInclude(ii => ii.Product)
                .FirstOrDefaultAsync(i => i.Id == invoiceId && i.UserId == userId);

            if (invoice == null) throw new Exception("");

            return new InvoiceDetailDto
            {
                InvoiceId = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Status = invoice.Status,
                TotalPrice = invoice.TotalPrice,
                CustomerName = invoice.Customer.FullName,
                Items = invoice.Items.Select(i => new InvoiceItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    DiscountPercent = i.DiscountPercent,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };


        }
    }
}
