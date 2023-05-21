using Enitities;

namespace MongoDb.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersByPageAsync(string? companyName, int page, int pageSize);
        Task<long> GetCountCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string Id);
        Task CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(string Id, Customer user);
        Task<bool> DeleteCustomerAsync(string Id);
    }
}
