using Enitities;

namespace MongoDb.Repositories
{
    public interface IMongoCustomerRepository
    {
        Task<long> GetCountCustomersAsync();
        Task<IEnumerable<Customer>> GetAllCustomersByPageAsync(string? companyName, int page, int pageSize);
        Task<Customer> GetCustomerByIdAsync(Guid Id);
        Task CreateCustomerAsync(Customer user);
        Task UpdateCustomerAsync(Customer user);
        Task DeleteCustomerAsync(Guid Id);
    }
}
