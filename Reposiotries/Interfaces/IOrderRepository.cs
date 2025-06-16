using OrderService.DataAccess.Entities;

namespace OrderService.Reposiotries.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);

    Task AddAsync(Order order);
}
