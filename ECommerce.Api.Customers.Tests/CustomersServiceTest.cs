using ECommerce.Api.Customers.Data.Entities;
using ECommerce.Api.Customers.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ECommerce.Api.Customers.Profiles;

namespace ECommerce.Api.Customers.Tests
{
    public class CustomersServiceTest
    {
        private readonly CustomerDbContext _dbContext;
        private readonly Mapper _mapper;
        private readonly CustomerRepo _customerRepo;

        public CustomersServiceTest()
        {
            //AAA - Arrange, Act, and Assert
            //DbContext
            var options = new DbContextOptionsBuilder<CustomerDbContext>() 
                .UseInMemoryDatabase("CustomerTests")
                .Options;
            _dbContext = new CustomerDbContext(options);
            CreateCustomers();

            //Mapper
            var customerProfile = new CustomerDtoProfile();
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(customerProfile));
            _mapper = new Mapper(configuration);

            //Create
            _customerRepo = new CustomerRepo(_mapper, null, _dbContext);
        }

        [Fact]
        public async Task CustomersServiceReturnsAllCustomers()
        {
            var customers = await _customerRepo.GetCustomersAsync();

            Assert.True(customers.IsSuccess);
            Assert.True(customers.CustomersDto.Any());
            Assert.Null(customers.ErrorMsg);
        }

        [Fact]
        public async Task CustomersServiceReturnsValidCustomer()
        {
            var aCustomer = await _customerRepo.GetCustomerByIdAsync(1);

            Assert.True(aCustomer.IsSuccess);
            Assert.NotNull(aCustomer.ACustomerDto);
            Assert.True(aCustomer.ACustomerDto.Id == 1);
            Assert.Null(aCustomer.ErrorMsg);
        }

        [Fact]
        public async Task CustomersServiceReturnsInValidCustomer()
        {
            var aCustomer = await _customerRepo.GetCustomerByIdAsync(-1);

            Assert.False(aCustomer.IsSuccess);
            Assert.Null(aCustomer.ACustomerDto);
            Assert.NotNull(aCustomer.ErrorMsg);
        }

        private void CreateCustomers()
        {
            if (!_dbContext.Customers.Any())
            {
                for (int i=1; i<10; i++)
                {
                    _dbContext.Customers.Add(
                        new Customer
                        {
                            Id = i,
                            Name = new Guid().ToString(),
                            Address = new Guid().ToString()
                        }
                    );
                }
            }
            _dbContext.SaveChanges();
        }
    }
}