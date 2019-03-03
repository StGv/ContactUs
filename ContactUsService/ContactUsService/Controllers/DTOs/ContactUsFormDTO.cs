
using System.ComponentModel.DataAnnotations;

namespace ContactUsService.Controllers.DTOs
{
    public class ContactUsFormDTO
    {
        [Required]
        [MaxLength(100)]
        public string fullName { get; set; }
        [Required]
        public string email { get; set; }
        [MaxLength(2500)]
        public string message { get; set; }
    }
}