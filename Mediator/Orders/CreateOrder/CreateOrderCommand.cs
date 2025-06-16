using MediatR;
using OrderService.Dtos;

namespace OrderService.Mediator.Order.CreateOrder;

public class CreateOrderCommand(CreateOrderDto dto) : IRequest<int>
{
    public CreateOrderDto dto { get; set; } = dto;
}