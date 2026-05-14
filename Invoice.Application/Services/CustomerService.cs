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
            customer.UserId = _httpContextAccessor.HttpContext.GetUserId();

            await _customerRepository.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return customer.Id;
        }

        public async Task<bool> UpdateAsync(UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(updateCustomerDto.CustomerId);

            if (existingCustomer == null) throw new Exception("مشتری پیدا نشد");
            if (existingCustomer.UserId != _httpContextAccessor.HttpContext.GetUserId())
                throw new Exception("مشتری مربوط به شما نمی باشد.");

            _mapper.Map(updateCustomerDto, existingCustomer);

            _customerRepository.Update(existingCustomer);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid customerId)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerId);

            if (existingCustomer == null) throw new Exception("مشتری پیدا نشد");
            if (existingCustomer.UserId != _httpContextAccessor.HttpContext.GetUserId())
                throw new Exception("مشتری مربوط به شما نمی باشد.");

            await _customerRepository.DeleteAsync(existingCustomer.Id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GetAllCustomersDto>> GetAllAsync(Guid? userId)
        {
            var customers = _customerRepository.EntitiesAsNoTracking;

            if (userId != Guid.Empty)
                customers = customers.Where(c => c.UserId == userId.Value);

            var doctorsProjected = await customers
                    .ProjectTo<GetAllCustomersDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            if (doctorsProjected == null || doctorsProjected.Count == 0)
                return new List<GetAllCustomersDto>();

            return doctorsProjected;
        }

        public async Task<GetCustomerDetailsDto> GetByIdAsync(Guid customerId)
        {
            var customer = await _customerRepository
                .EntitiesAsNoTracking.Where(c => c.Id == customerId)
                .ProjectTo<GetCustomerDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (customer == null) return new GetCustomerDetailsDto();

            return customer;
        }
    }
}
