using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Entities;

namespace OrderService.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders {  get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(r=>r.Id);
        });
    }
}
