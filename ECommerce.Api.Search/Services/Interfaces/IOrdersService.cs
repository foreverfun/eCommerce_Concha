using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMsg)> GetOrdersAsync(int customerId);
    }
}
