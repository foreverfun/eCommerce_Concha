using ECommerce.Api.Customers.Repositories;
using ECommerce.Api.Customers.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomersController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _customerRepo.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.CustomersDto);
            }
            return BadRequest(result.ErrorMsg);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            var result = await _customerRepo.GetCustomerByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.ACustomerDto);
            }
            return BadRequest(result.ErrorMsg);
        }
    }
}
