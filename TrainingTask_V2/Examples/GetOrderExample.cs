using OrderService.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace OrderService.Examples;

public class GetOrderExample : IExamplesProvider<OrderDto>
{
    public OrderDto GetExamples()
    {
        return new OrderDto()
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            ProductId = 8,
            Quantity = 10
        };
    }
}