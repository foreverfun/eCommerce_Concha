using ECommerce.Api.Products.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
