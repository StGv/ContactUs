using ContactUsService.Models;
using System.Threading.Tasks;

namespace ContactUsService.Services
{
    public interface ICustomerMessageRepository
    {
        Task<CustomerMessage> GetCustomerMessageAsync(int id);
        Task<CustomerMessage> CreateNewMessageAsync(CustomerMessage message);
    }
}
