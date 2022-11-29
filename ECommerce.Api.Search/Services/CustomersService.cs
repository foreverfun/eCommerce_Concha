using ECommerce.Api.Search.Models;
using ECommerce.Api.Search.Services.Interfaces;
using System.Drawing;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<CustomersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, Customer customer, string ErrorMessage)> GetCustomerAsync(int customerId)
        {
            try
            { 
                var client = _httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"/api/Customers/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var customerResult = JsonSerializer.Deserialize<Customer>(content, options);
                    return (true, customerResult, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
