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

        //done
        public async Task<Guid> CreateAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            customer.UserId = _httpContextAccessor.HttpContext.GetUserId();

            await _customerRepository.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return customer.Id;
        }

        //done
        public async Task<bool> UpdateAsync(UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(updateCustomerDto.CustomerId);

            if (existingCustomer == null) throw new Exception("مشتری پیدا نشد");
            if (existingCustomer.UserId != _httpContextAccessor.HttpContext.GetUserId())
                throw new Exception("");

            _mapper.Map(updateCustomerDto, existingCustomer);

            _customerRepository.Update(existingCustomer);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        //done
        public async Task<bool> DeleteAsync(Guid customerId)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerId);

            if (existingCustomer == null) throw new Exception("مشتری پیدا نشد");
            if (existingCustomer.UserId != _httpContextAccessor.HttpContext.GetUserId())
                throw new Exception("");

            await _customerRepository.DeleteAsync(existingCustomer.Id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        //done
        public async Task<IEnumerable<GetAllCustomersDto>> GetAllAsync(Guid? userId)
        {
            var customers = _customerRepository.EntitiesAsNoTracking;

            if (userId != Guid.Empty)
                customers = customers.Where(c => c.UserId == userId.Value);

            var customersList = await customers.ToListAsync();

            if (customersList == null || customersList.Count() == 0)
                return new List<GetAllCustomersDto>();

            return _mapper.Map<IEnumerable<GetAllCustomersDto>>(customersList);

        }

        //done
        public async Task<GetCustomerDetailsDto> GetByIdAsync(Guid customerId)
        {
            var customer = await _customerRepository.EntitiesAsNoTracking.FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null) return new GetCustomerDetailsDto();

            return _mapper.Map<GetCustomerDetailsDto>(customer);
        }


    }
}
