using AutoMapper;
using ECommerce.Api.Customers.Data.Entities;
using ECommerce.Api.Customers.Models;
using ECommerce.Api.Customers.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerRepo> _logger;
        private readonly CustomerDbContext _context;

        public CustomerRepo(IMapper mapper, ILogger<CustomerRepo> logger, CustomerDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;

            SeedData();
        }

        public async Task<(bool IsSuccess, CustomerDto ACustomerDto, string ErrorMsg)> GetCustomerByIdAsync(int id)
        {
            try 
            { 
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    var result = _mapper.Map<CustomerDto>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch(Exception ex )
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerDto> CustomersDto, string ErrorMsg)> GetCustomersAsync()
        {
            try 
            { 
                var customers = await _context.Customers.ToListAsync();
                if (customers != null || customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer { Id = 1, Name = "John", Address = "1 John Drive"});
                _context.Customers.Add(new Customer { Id = 2, Name = "Mike", Address = "2 Mike Loop" });
                _context.Customers.Add(new Customer { Id = 3, Name = "Siri", Address = "3 Siri Street" });

                _context.SaveChanges();
            }
        }
    }
}
