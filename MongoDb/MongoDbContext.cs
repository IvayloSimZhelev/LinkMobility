using Enitities;
using Microsoft.EntityFrameworkCore;

namespace MongoDb
{
    public class MongoDbContext : DbContext
    {
        public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
        {
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);
        }
    }
}
