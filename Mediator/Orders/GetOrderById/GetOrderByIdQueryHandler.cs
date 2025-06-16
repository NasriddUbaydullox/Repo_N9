using MediatR;
using OrderService.Dtos;
using OrderService.Mediator.Order.GetOrders;
using OrderService.Reposiotries.Interfaces;

namespace OrderService.Mediator.Order.GetOrderById;

public class GetOrderByIdQueryHandler(IOrderRepository repo) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(request.id);

        if (order == null)
            throw new Exception("Order Not Found");

        return new OrderDto()
        {
            Id = order.Id,  
            CreatedAt = order.CreatedAt,    
            ProductId = order.ProductId,
            Quantity = order.Quantity
        };
    }
}
