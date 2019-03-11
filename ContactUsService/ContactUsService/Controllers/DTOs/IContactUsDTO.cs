
namespace ContactUsService.Controllers.DTOs
{
    public interface IContactUsDTO
    {
        string fullName { get; }
        string email { get; }
        string message { get; }
    }
}
