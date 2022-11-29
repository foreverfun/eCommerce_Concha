using ECommerce.Api.Products.Data.Entities;
using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Profiles
{
    public class ProductDtoProfile : AutoMapper.Profile
    {
        public ProductDtoProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
