using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess.Entities;

namespace ProductService.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasKey(r => r.Id);
        });
    }
}