using AutoMapper;
using ECommerce.Api.Products.Data;
using ECommerce.Api.Products.Data.Entities;
using ECommerce.Api.Products.Models;
using ECommerce.Api.Products.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ECommerce.Api.Products.Repositories
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductsRepo> _logger;
        private readonly IMapper _mapper;

        public ProductsRepo(ProductsDbContext dbContext, ILogger<ProductsRepo> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            
            SeedData();
        }

        public async Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync(productId);
                if (product != null)
                {
                    var result = _mapper.Map<ProductDto>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<ProductDto>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
                
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
            
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            { 
                _dbContext.Products.Add(new Product { Id = 1, Name = "Keyboard", Price = 20, Inventory = 750});
                _dbContext.Products.Add(new Product { Id = 2, Name = "Mouse", Price = 5, Inventory = 500 });
                _dbContext.Products.Add(new Product { Id = 3, Name = "Monitor", Price = 150, Inventory = 200 });
                _dbContext.Products.Add(new Product { Id = 4, Name = "CPU", Price = 200, Inventory = 500 });
                _dbContext.SaveChanges();
            }
        }
    }
}
