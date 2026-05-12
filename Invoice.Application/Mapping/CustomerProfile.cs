namespace Invoice.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetAllCustomersDto>();
            CreateMap<Customer, GetCustomerDetailsDto>();
            CreateMap<CreateCustomerDto, Customer>(); 
            CreateMap<UpdateCustomerDto, Customer>();
        }
    }
}
