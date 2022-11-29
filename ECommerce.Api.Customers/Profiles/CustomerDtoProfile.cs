using ECommerce.Api.Customers.Data.Entities;
using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerDtoProfile : AutoMapper.Profile
    {
        public CustomerDtoProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
