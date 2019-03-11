using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ContactUsService.Models
{
    public class Customer
    {
        private Customer()
        {
        }

        [Key]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        public static Customer Create(string email , string fullName)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Value cannot be empty", "Email");

            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentException("Value cannot be empty", "Full Name");

            return new Customer()
            {
                Email = email,
                FirstName = getFirstName(fullName),
                LastName = getLastName(fullName)
            };
        }

        public static string getFirstName(string fullname)
        {
            return fullname.Trim().Split(' ')[0];
        }
        public static string getLastName(string fullname)
        {
            return string.Join(" ", fullname.Trim().Split(' ').Skip(1).Where(n => !string.IsNullOrWhiteSpace(n)));
        }
    }
}