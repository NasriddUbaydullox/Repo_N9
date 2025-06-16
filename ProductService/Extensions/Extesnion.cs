using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess;
using ProductService.Reposiotries;
using ProductService.Reposiotries.Interfaces;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace ProductService.Extensions;

public static class Extesnion
{
    public static IServiceCollection AddProducts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddSwaggerGen(options =>
        {
            options.ExampleFilters(); 
        });

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

        services.AddMediatR(r => r.RegisterServicesFromAssemblyContaining(typeof(Program)));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
