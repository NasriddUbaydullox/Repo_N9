using MediatR;
using OrderService.Dtos;
using OrderService.DataAccess.Entities;
using OrderService.Reposiotries.Interfaces;
using OrderService.Mediator.Order.CreateOrder;
using OrderService.ProductClients;

public class CreateOrderCommandHandler(
    IOrderRepository repo,
    IProductClient productClient
) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var product = await productClient.GetProductByIdAsync(request.dto.ProductId);
        if (product == null)
            throw new Exception("Product not found"); // I know this is incorrect version but i didn't want to write this code(all if elses ) in controller! that would be very ugly.

        if (product.Stock < request.dto.Quantity)
            throw new Exception("Not enough stock");

        var updatedStock = product.Stock - request.dto.Quantity;
        var updateSuccess = await productClient.UpdateProductStockAsync(product.Id, updatedStock);

        if (!updateSuccess)
            throw new Exception("Failed to update stock");

        var order = new Order
        {
            ProductId = request.dto.ProductId,
            Quantity = request.dto.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        await repo.AddAsync(order);
        return order.Id;
    }
}