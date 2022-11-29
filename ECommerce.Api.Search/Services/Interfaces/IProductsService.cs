using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMsg)> GetProductAsync();
    }
}
