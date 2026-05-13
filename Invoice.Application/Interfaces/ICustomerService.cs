namespace Invoice.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<GetAllCustomersDto>> GetAllAsync(Guid? userId);

        Task<GetCustomerDetailsDto> GetByIdAsync(Guid customerId);

        Task<Guid> CreateAsync(CreateCustomerDto createCustomerDto);

        Task<bool> UpdateAsync(UpdateCustomerDto updateCustomerDto);

        Task<bool> DeleteAsync(Guid customerId);
    }
}
