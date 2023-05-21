using Enitities;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace MongoDb.Repositories
{
    public class MongoCustomerRepository : IMongoCustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        public MongoCustomerRepository(IMongoClient client)
        {
            var database = client.GetDatabase("LinkMobilityDB");
            _customers = database.GetCollection<Customer>("Customers");
        }

        public async Task<long> GetCountCustomersAsync()
        {
            var filter = Builders<Customer>.Filter.Ne(x => x.Id, null);
            return await _customers.CountDocumentsAsync(filter);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersByPageAsync(string? companyName, int page,  int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var filter = companyName == null
                ? Builders<Customer>.Filter.Empty // If companyName is null not accepted it 
                : Builders<Customer>.Filter.Eq(x => x.CompanyName, companyName);

            return await _customers.Find(filter)
                                   .Skip(skip)
                                   .Limit(pageSize)
                                   .ToListAsync();
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            try
            {
                if (!IsValidHex(customer.Id))
                {
                    customer.Id = ObjectId.GenerateNewId().ToString();
                }

                await _customers.InsertOneAsync(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        private bool IsValidHex(string hexString)
        {
            if (hexString.Length != 24)
                return false;

            foreach (char c in hexString)
            {
                if (!IsHexDigit(c))
                    return false;
            }

            return true;
        }

        private bool IsHexDigit(char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }
        public async Task<bool> DeleteCustomerAsync(string Id)
        {
            var deleteResult = await _customers.DeleteOneAsync(u => u.Id == Id);
            return deleteResult.DeletedCount > 0;
        }


        public async Task<Customer> GetCustomerByIdAsync(string Id)
        {
            return await _customers.Find(u => u.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCustomerAsync(string Id, Customer customer)
        {
            try
            {
                var updateResult = await _customers.UpdateOneAsync(
               u => u.Id == Id,
               Builders<Customer>.Update
                   .Set(u => u.Address, customer.Address)
                   .Set(u => u.State, customer.State)
                   .Set(u => u.Country, customer.Country)
                   .Set(u => u.CompanyName, customer.CompanyName)
                );
               return updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
