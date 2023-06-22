
using Enitities;
using MongoDB.Driver;

namespace MongoDb
{
    public class Seeder
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Customer> _customers;
        private readonly IMongoCollection<Invoice> _invoices;

        public Seeder(IMongoClient client)
        {
            var database = client.GetDatabase("LinkMobilityDB");
            _users = database.GetCollection<User>("Users");
            _customers = database.GetCollection<Customer>("Customers");
            _invoices = database.GetCollection<Invoice>("Invoices");
        }

        public void Seed()
        {
            SeedUser();
            SeedCustomer();
        }

        private void SeedCustomer()
        {

            if(_customers.Find(custommer => true).Any())
            {
                return; // Базата от данни вече е попълнена
            }

            var customers = new List<Customer>();

            for(int i = 0; i < 1010; i++)
            {
                var invoices = new List<Invoice>();

                for(int j = 0; j < 2; j++)
                {
                    invoices.Add(new Invoice
                    {
                        Id = Guid.NewGuid(),
                        InvoiceNumber = $"INV00{i}",
                        Date = DateTime.UtcNow.AddDays(-j),
                        Total = new Random().Next(100, 1000)
                    });
                }

                string[] subscriptionStates = { "New", "Active", "Suspended" };
                int randomIndex = new Random().Next(0, subscriptionStates.Length);
                string subscriptionState = subscriptionStates[randomIndex];

                customers.Add(new Customer
                {
                    Id = Guid.NewGuid(),
                    CompanyName = $"Company{i}",
                    Address = $"Address{i}",
                    State = $"State{i}",
                    Country = $"Country{i}",
                    SubscriptionState = subscriptionState,
                    NumberOfInvoices = invoices.Count,
                    Invoice = invoices
                });
            }


            // Вмъкване на данните в базата данни
            _customers.InsertMany(customers);

        }

        private void SeedUser()
        {
            if(_users.Find(user => true).Any())
            {
                return; // Базата от данни вече е попълнена
            }

            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "John",  Email = "John@gmail.com", Password = "Demos2"},
                new User { Id = Guid.NewGuid(), Name = "Ivan",  Email = "Ivan@gmail.com", Password = "Demos1"},
                new User { Id = Guid.NewGuid(), Name = "Ivaylo",  Email = "ivaylo.sim.zhelev@gmail.com", Password = "Demos3"},
            };

            _users.InsertMany(users);
        }
    }
}
