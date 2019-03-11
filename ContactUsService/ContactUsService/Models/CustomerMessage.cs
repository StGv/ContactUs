using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactUsService.Models
{
    public class CustomerMessage
    {
        [Key]
        public int Id { get; private set; }

        [MaxLength(2500)]
        public string Text { get; private set; }

        [ForeignKey("CustomerEmail")]
        public string CustomerEmail { get { return Customer.Email; } }

        [Required]
        public DateTime ReceivedOn { get; private set; }

        public virtual Customer Customer { get; private set; }

        private CustomerMessage()
        {
        }

        //FactoryMethod
        public static CustomerMessage Create(string message, string email, string fullName)
        {
            return new CustomerMessage()
            {
                Text = message,
                ReceivedOn = DateTime.UtcNow,
                Customer = Customer.Create(email, fullName)
            };
        }

        public static CustomerMessage Update(string message, Customer customer)
        {
            return new CustomerMessage()
            {
                Text = message,
                ReceivedOn = DateTime.UtcNow,
                Customer = customer
            };
        }
    }
}