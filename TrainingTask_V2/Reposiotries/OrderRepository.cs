using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess;
using OrderService.DataAccess.Entities;
using OrderService.Reposiotries.Interfaces;

namespace OrderService.Reposiotries;

public class OrderRepository(ApplicationDbContext context) : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        await context.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await context.Orders.FindAsync(id);
    }
}
