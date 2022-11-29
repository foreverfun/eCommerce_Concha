using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Repositories.Interfaces
{
    public interface IProductsRepo
    {
        Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductByIdAsync(int productId);
    }
}
