using OrderService.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace OrderService.Examples;

public class CreateOrderExample : IExamplesProvider<CreateOrderDto>
{
    public CreateOrderDto GetExamples()
    {
        return new CreateOrderDto()
        {
            ProductId = 1,
            Quantity = 10,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
