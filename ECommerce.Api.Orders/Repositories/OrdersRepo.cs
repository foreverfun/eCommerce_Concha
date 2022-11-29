using AutoMapper;
using ECommerce.Api.Orders.Data;
using ECommerce.Api.Orders.Data.Entities;
using ECommerce.Api.Orders.Models;
using ECommerce.Api.Orders.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Drawing;

namespace ECommerce.Api.Orders.Repositories
{
    public class OrdersRepo : IOrdersRepo
    {
        private readonly OrdersDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersRepo> _logger;

        public OrdersRepo(OrdersDbContext context, ILogger<OrdersRepo> logger, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

            SeedData();
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderDto> OrdersDto, string ErrorMsg)> GetOrderByIdAsync(int customerId)
        {
            try 
            {
                var order = await _context.Orders.Include(o=>o.Items ).Where(o=>o.CustomerId == customerId).ToListAsync();
                var result = _mapper.Map<IEnumerable<OrderDto>>(order);
                if (order != null && order.Count() != 0)
                    return (true, result, null);
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderDto> OrdersDto, string ErrorMsg)> GetOrdersAsync()
        {
            try
            {
                var orders = await _context.Orders.Include(o => o.Items).ToListAsync();
                if (orders.Any() && orders != null)
                {
                    var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!_context.Orders.Any())
            {
                _context.Orders.Add(new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-10),
                    Total = 350,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 3,
                            Quantity = 1,
                            UnitPrice = 150
                        },
                        new OrderItem
                        {
                            Id = 2,
                            OrderId = 1,
                            ProductId = 4,
                            Quantity = 1,
                            UnitPrice = 200
                        }
                    }
                });

                _context.Orders.Add(new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now.AddDays(-5),
                    Total = 100,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Id = 3,
                            OrderId = 2,
                            ProductId = 1,
                            Quantity = 5,
                            UnitPrice = 20
                        }
                    }
                });

                _context.Orders.Add(new Order
                {
                    Id = 3,
                    CustomerId = 3,
                    OrderDate = DateTime.Now,
                    Total = 25,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Id = 4,
                            OrderId = 3,
                            ProductId = 1,
                            Quantity = 1,
                            UnitPrice = 20
                        },
                        new OrderItem
                        {
                            Id = 5,
                            OrderId = 3,
                            ProductId = 2,
                            Quantity = 1,
                            UnitPrice = 5
                        }
                    }
                });
               
                _context.SaveChanges();
            }
                
        }
    }
}
