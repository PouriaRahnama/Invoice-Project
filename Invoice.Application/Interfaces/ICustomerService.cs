namespace Invoice.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<SearchQueryResponse<GetAllCustomersDto>> GetAllAsync(FilterCustomersDto QueryParams);

        Task<GetCustomerDetailsDto> GetByIdAsync(Guid customerId);

        Task<Guid> CreateAsync(CreateCustomerDto createCustomerDto);

        Task<bool> UpdateAsync(UpdateCustomerDto updateCustomerDto);

        Task<bool> DeleteAsync(Guid customerId);
    }
}
