using AutoMapper;
using ECommerce.Api.Orders.Models;
using ECommerce.Api.Orders.Repositories;
using ECommerce.Api.Orders.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersRepo _ordersRepo;

        public OrderController(IOrdersRepo ordersRepo)
        {
            _ordersRepo = ordersRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await _ordersRepo.GetOrdersAsync();
            if (result.IsSuccess)
                return Ok(result.OrdersDto);
            return BadRequest(result.ErrorMsg);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int customerId)
        {
            var result = await _ordersRepo.GetOrderByIdAsync(customerId);
            if (result.IsSuccess)
                return Ok(result.OrdersDto);
            return BadRequest(result.ErrorMsg);
        }

    }
}
