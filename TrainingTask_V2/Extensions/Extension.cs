using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess;
using OrderService.Reposiotries.Interfaces;
using OrderService.Reposiotries;
using OrderService.ProductClients;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using Serilog;

namespace OrderService.Extensions;

public static class Extension
{
    public static IServiceCollection AddOrders(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddHttpClient<IProductClient,ProductClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7234/");
        });

        services.AddSwaggerGen(options =>
        {
            options.ExampleFilters();
        });

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console().WriteTo
            .File("Logs/log-.txt", rollingInterval: RollingInterval.Day).Enrich
            .FromLogContext().MinimumLevel
            .Information()
            .CreateLogger();

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

        services.AddMediatR(r => r.RegisterServicesFromAssemblyContaining(typeof(Program)));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
