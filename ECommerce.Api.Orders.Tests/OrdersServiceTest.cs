using ECommerce.Api.Orders.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ECommerce.Api.Orders.Profiles;
using ECommerce.Api.Orders.Data.Entities;
using ECommerce.Api.Orders.Repositories;

namespace ECommerce.Api.Orders.Tests
{
    public class OrdersServiceTest
    {
        private readonly OrdersDbContext _dbContext;
        private readonly Mapper _mapper;
        private readonly OrdersRepo _orderRepo;

        public OrdersServiceTest()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase("OrdersDatabseTest")
                .Options;
            _dbContext = new OrdersDbContext(options);
            CreateOrder();

            //AutoMapper = create a profile, configuration to add the profile, mapper
            var orderProfile = new OrderDtoProfile();
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(orderProfile));
            _mapper = new Mapper(configuration);

            //create a repo
            _orderRepo = new OrdersRepo(_dbContext, null, _mapper);
        }

        [Fact]
        public async Task GetOrdersReturnAllOrders()
        {
            var orders = await _orderRepo.GetOrdersAsync();

            //Assert
            Assert.True(orders.IsSuccess);
            Assert.NotNull(orders.OrdersDto);
            Assert.True(orders.OrdersDto.Any());
            Assert.Null(orders.ErrorMsg);

        }

        [Fact]
        public async Task GetOrdersByIdReturnValidOrders()
        {
            var orders = await _orderRepo.GetOrderByIdAsync(1);

            Assert.True(orders.IsSuccess);
            Assert.NotNull(orders.OrdersDto);
            Assert.True(orders.OrdersDto.Any());
            Assert.Null(orders.ErrorMsg);
        }

        [Fact]
        public async Task GetOrdersByIdReturnInvalidOrders()
        {
            var orders = await _orderRepo.GetOrderByIdAsync(-1);

            Assert.False(orders.IsSuccess);
            Assert.Null(orders.OrdersDto);
            Assert.NotNull(orders.ErrorMsg);
        }

        private void CreateOrder()
        {
            if (!_dbContext.Orders.Any())
            {
                for (int i=1; i<10; i++)
                {
                    _dbContext.Orders.Add(
                        new Order
                        {
                            Id = i,
                            CustomerId = i,
                            OrderDate = DateTime.Now,
                            Total = Convert.ToDecimal(i * 10 * 3.14),
                            Items = new List<OrderItem> 
                            { 
                                new OrderItem
                                {
                                    Id = i,
                                    OrderId = i,
                                    ProductId = i,
                                    Quantity = i*10,
                                    UnitPrice = Convert.ToDecimal(i * 3.14)
                                }
                            }
                        }); ;
                }
                _dbContext.SaveChanges();
            }
        }
    }
}