using AutoMapper;
using ECommerce.Api.Products.Data;
using ECommerce.Api.Products.Data.Entities;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerce.Api.Products.Tests
{
    public class ProdcutsServiceTest
    {
        private readonly Mapper _mapper;
        private ProductsRepo _productsRepo;
        private readonly ProductsDbContext _dbContext;

        public ProdcutsServiceTest()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                    .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                    .Options;
            _dbContext = new ProductsDbContext(options);
            CreateProducts(_dbContext);

            //AutoMapper (Arrange)
            var productProfile = new ProductDtoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            _mapper = new Mapper(configuration);

            //Repo (Arrange)
            _productsRepo = new ProductsRepo(_dbContext, null, _mapper);
        }

        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            //Repo(Act)
            var products = await _productsRepo.GetProductsAsync();

            //Assert
            Assert.True(products.IsSuccess);
            Assert.True(products.Products.Any());
            Assert.Null(products.ErrorMessage);
        }

        
        [Fact]
        public async Task GetProductByIdReturnsValidId()
        {
            //Repo (Act)
            var aProduct = await _productsRepo.GetProductByIdAsync(1);

            //Assert
            Assert.True(aProduct.IsSuccess);
            Assert.NotNull(aProduct.Product);
            Assert.True(aProduct.Product.Id == 1);
            Assert.Null(aProduct.ErrorMessage);
        }

        [Fact]
        public async Task GetProductByIdReturnsInvalidId()
        {
            //Repo (Act)
            var aProduct = await _productsRepo.GetProductByIdAsync(-1);

            //Assert
            Assert.False(aProduct.IsSuccess);
            Assert.Null(aProduct.Product);
            Assert.NotNull(aProduct.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            if (!dbContext.Products.Any())
            { 
                for (int i=1; i<10; i++)
                {
                    dbContext.Products.Add(
                        new Product()
                        {
                            Id = i,
                            Name = Guid.NewGuid().ToString(),
                            Inventory = i + 100,
                            Price = (decimal)(i * 3.14)
                        });
                }
            }
            dbContext.SaveChanges();
        }
    }
}