namespace Invoice.Application.GridifyMappers
{
    public class CustomerGridifyMapper : GridifyMapper<GetAllCustomersDto>
    {
        public CustomerGridifyMapper()
        {
            AddMap("FullName", p => p.FullName);
            AddMap("Phone", p => p.Phone);
            AddMap("UserId", p => p.UserId);
            AddMap("CustomerId", p => p.CustomerId);
            AddMap("Address", p => p.Address);
        }
    }

    public class GetCustomersGridifyMapper : GridifyMapper<GetCustomersDto>
    {
        public GetCustomersGridifyMapper()
        {
            AddMap("FullName", p => p.FullName);
        }
    }
}
