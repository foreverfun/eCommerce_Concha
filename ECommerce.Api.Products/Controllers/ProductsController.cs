using ECommerce.Api.Products.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepo _productsRepo;

        public ProductsController(IProductsRepo productsRepo)
        {
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await _productsRepo.GetProductsAsync();
            if (result.IsSuccess)
                return Ok(result.Products);
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var result = await _productsRepo.GetProductByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Product);
            return NotFound();
        }
    }
}
