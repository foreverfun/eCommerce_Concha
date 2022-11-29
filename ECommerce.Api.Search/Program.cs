using ECommerce.Api.Search.Services;
using ECommerce.Api.Search.Services.Interfaces;
using Polly;

namespace ECommerce.Api.Search
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();

            builder.Services
                .AddHttpClient("ProductsService", config =>
                {
                    config.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

            builder.Services
                .AddHttpClient("OrdersService", config =>
                {
                    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

            builder.Services
                .AddHttpClient("CustomersService", config =>
                {
                    config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]);
                })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}