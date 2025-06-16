using MediatR;
using OrderService.Dtos;

namespace OrderService.Mediator.Order.GetOrders;

public record GetOrderByIdQuery(int id) : IRequest<OrderDto>;
