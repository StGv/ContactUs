using ContactUsService.Models;
using System.Data.Entity;

namespace ContactUsService.Contexts
{
    public class ContactUsDbContext : DbContext
    {
        public ContactUsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { 
        }

        public DbSet<CustomerMessage> Messages { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}