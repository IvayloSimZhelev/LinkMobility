using Enitities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace MongoDb.Repositories
{
    public class MongoCustomerRepository : IMongoCustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;
        private const string _collectionName = "Customers";
        private const string _databaseName = "LinkMobilityDB";

        public MongoCustomerRepository(IMongoClient client)
        {
            var database = client.GetDatabase(_databaseName);
            _customers = database.GetCollection<Customer>(_collectionName);
        }

        public async Task<long> GetCountCustomersAsync()
        {
            var filter = Builders<Customer>.Filter.Ne(x => x.Id, Guid.Empty);
            return await _customers.CountDocumentsAsync(filter);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersByPageAsync(string? companyName, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var filter = companyName == null
                ? Builders<Customer>.Filter.Empty // If companyName is null not accepted it 
                : Builders<Customer>.Filter.Eq(x => x.CompanyName, companyName);

            return (await _customers.Find(filter)
                                   .Skip(skip)
                                   .Limit(pageSize)
                                   .ToListAsync());
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            await _customers.InsertOneAsync(customer);
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid Id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(customer => customer.Id, Id);
            return await _customers.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(x => x.Id, customer.Id);
            await _customers.ReplaceOneAsync(filter, customer);
        }

        public async Task DeleteCustomerAsync(Guid Id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(customer => customer.Id, Id);
            await _customers.DeleteOneAsync(filter);
        }

    }
}
