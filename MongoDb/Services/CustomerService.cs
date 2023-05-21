using Enitities;
using Microsoft.Extensions.Configuration;
using MongoDb.Repositories;

namespace MongoDb.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCustomerRepository _mongoCustomerRepository;
        private readonly IConfiguration _configuration;

        public CustomerService(IMongoCustomerRepository mongoCustomerRepository, IConfiguration configuration)
        {
            _mongoCustomerRepository = mongoCustomerRepository;
            _configuration = configuration;
        }

        public async Task<long> GetCountCustomersAsync()
        {
            return await _mongoCustomerRepository.GetCountCustomersAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersByPageAsync(string? companyName, int page, int pageSize)
        {
            return await _mongoCustomerRepository.GetAllCustomersByPageAsync(companyName, page, pageSize);
        }

        public async Task<Customer> GetCustomerByIdAsync(string Id)
        {
            return await _mongoCustomerRepository.GetCustomerByIdAsync(Id);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _mongoCustomerRepository.CreateCustomerAsync(customer);
        }

        public async Task<bool> UpdateCustomerAsync(string Id, Customer customer)
        {
            return await _mongoCustomerRepository.UpdateCustomerAsync(Id, customer);
        }

        public async Task<bool> DeleteCustomerAsync(string Id)
        {
            return await _mongoCustomerRepository.DeleteCustomerAsync(Id);
        }

        
    }
}
