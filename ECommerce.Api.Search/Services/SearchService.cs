using ECommerce.Api.Search.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;
        private readonly ILogger<SearchService> _logger;

        public SearchService(
            IOrdersService ordersService, 
            IProductsService productsService, 
            ICustomersService customersService, 
            ILogger<SearchService> logger)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customersService = customersService;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            try 
            { 
                var ordersResult = await _ordersService.GetOrdersAsync(customerId);
                var customerResult = await _customersService.GetCustomerAsync(customerId);
                var productsResult = await _productsService.GetProductAsync();
           
                if (ordersResult.IsSuccess)
                {
                    foreach(var order in ordersResult.Orders)
                    {   
                        foreach (var item in order.Items)
                        {
                            item.ProductName = productsResult.IsSuccess ?
                                productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                                "Product information is not available";
                        }
                    }

                    if (customerResult.IsSuccess)
                    { 
                        var result = new
                        {
                            Orders = ordersResult.Orders,
                            Customer = customerResult.customer
                        };
                        return (true, result);
                    }
                    else
                    {
                        var result = new
                        {
                            Orders = ordersResult.Orders,
                            Customer = "Customer Information is not available"
                        };
                        return (true, result);
                    }
                }
                return (false, null);
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
