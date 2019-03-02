using System.ComponentModel.DataAnnotations;

namespace ContactUsService.Models
{
    public class Customer
    {
        [Key]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}