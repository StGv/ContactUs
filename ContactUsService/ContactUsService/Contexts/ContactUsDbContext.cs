using ContactUsService.Models;
using System.Data.Common;
using System.Data.Entity;

namespace ContactUsService.Contexts
{
    public class ContactUsDbContext : DbContext
    {
        public ContactUsDbContext(DbConnection connection)
        : base(connection, true)
        {
        }
        
        public ContactUsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { 
        }

        public DbSet<CustomerMessage> Messages { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}