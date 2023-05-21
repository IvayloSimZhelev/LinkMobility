using Enitities;

namespace MongoDb.Repositories
{
    public interface IMongoCustomerRepository
    {
        Task<long> GetCountCustomersAsync();
        Task<IEnumerable<Customer>> GetAllCustomersByPageAsync(string? companyName, int page, int pageSize);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string Id);
        Task CreateCustomerAsync(Customer user);
        Task<bool> UpdateCustomerAsync(string Id, Customer user);
        Task<bool> DeleteCustomerAsync(string Id);        
    }
}
