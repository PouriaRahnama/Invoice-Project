using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Invoice.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerService(ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this._customerRepository = customerRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> CreateAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            if (userId == null || userId == Guid.Empty)
                throw new UnauthorizedException("کاربر شناسایی نشد");

            customer.UserId = userId;

            await _customerRepository.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return customer.Id;
        }

        public async Task<bool> UpdateAsync(UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(updateCustomerDto.CustomerId);

            if (existingCustomer == null) throw new NotFoundException("مشتری پیدا نشد");

            if (existingCustomer.UserId != _httpContextAccessor.HttpContext.GetUserId())
                throw new BusinessException("مشتری مربوط به شما نمی باشد.");

            _mapper.Map(updateCustomerDto, existingCustomer);

            _customerRepository.Update(existingCustomer);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid customerId)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerId);
            if (existingCustomer == null) throw new NotFoundException("مشتری پیدا نشد");

            if (existingCustomer.UserId != _httpContextAccessor.HttpContext.GetUserId())
                throw new BusinessException("مشتری مربوط به شما نمی باشد.");

            await _customerRepository.DeleteAsync(existingCustomer.Id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<SearchQueryResponse<GetAllCustomersDto>> GetAllAsync(FilterCustomersDto QueryParams)
        {
            var mapper = new CustomerGridifyMapper();
            var query = _customerRepository.EntitiesAsNoTracking
                    .ProjectTo<GetAllCustomersDto>(_mapper.ConfigurationProvider)
                     .OrderByDescending(x => EF.Property<DateTime>(x, "CreatedDateTime"))
                     .AsQueryable();

            var qp = await query.GridifyQueryableAsync(QueryParams, mapper);

            var pq = new Paging<GetAllCustomersDto>(qp.Count, qp.Query);
            return new SearchQueryResponse<GetAllCustomersDto>(QueryParams, pq);
        }

        public async Task<GetCustomerDetailsDto> GetByIdAsync(Guid customerId)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            if (userId == null || userId == Guid.Empty)
                throw new UnauthorizedException("کاربر شناسایی نشد");

            var customer = await _customerRepository
                .EntitiesAsNoTracking.Where(c => c.Id == customerId )
                .ProjectTo<GetCustomerDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (customer == null) throw new NotFoundException("مشتری پیدا نشد.");

            return customer;
        }
    }
}
