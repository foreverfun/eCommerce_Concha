using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        Task<(bool IsSuccess, IEnumerable<CustomerDto> CustomersDto, string ErrorMsg)> GetCustomersAsync();
        Task<(bool IsSuccess, CustomerDto ACustomerDto, string ErrorMsg)> GetCustomerByIdAsync(int id);
    }
}
