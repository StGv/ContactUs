using System.ComponentModel.DataAnnotations;

namespace ContactUsService.Models
{
    public class Customer
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}