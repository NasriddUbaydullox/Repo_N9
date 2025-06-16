using ProductService.DataAccess.Entities;

namespace ProductService.Reposiotries.Interfaces;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task SaveChangesAsync();
}
