using ECommerce.Api.Orders.Data.Entities;
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderDtoProfile : AutoMapper.Profile
    {
        public OrderDtoProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
            
            CreateMap<Order, OrderDto>();
        }
    }
}
