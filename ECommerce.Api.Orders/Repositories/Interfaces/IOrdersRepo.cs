using ECommerce.Api.Orders.Models;
using System.Collections;

namespace ECommerce.Api.Orders.Repositories.Interfaces
{
    public interface IOrdersRepo
    {
        Task<(bool IsSuccess, IEnumerable<OrderDto> OrdersDto, string ErrorMsg)> GetOrdersAsync();
        Task<(bool IsSuccess, IEnumerable<OrderDto> OrdersDto, string ErrorMsg)> GetOrderByIdAsync(int customerId);
    }
}
