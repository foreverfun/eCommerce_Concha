using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, Customer customer, string ErrorMessage)> GetCustomerAsync(int customerId);
    }
}
