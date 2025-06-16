using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess;
using ProductService.DataAccess.Entities;
using ProductService.Reposiotries.Interfaces;

namespace ProductService.Reposiotries;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task AddAsync(Product product)
    {
        await context.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task UpdateAsync(Product product)
    {
        context.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}