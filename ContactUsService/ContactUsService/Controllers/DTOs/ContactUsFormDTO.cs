
using System.ComponentModel.DataAnnotations;

namespace ContactUsService.Controllers.DTOs
{
    public class ContactUsFormDTO : IContactUsDTO
    {
        [Required]
        [MaxLength(100)]
        public string fullName { get; set; }
        [Required]
        [MaxLength(128)]
        [EmailAddress]
        public string email { get; set; }
        [MaxLength(2500)]
        public string message { get; set; }
    }
}