using MediatR;

namespace ProductService.Mediator.Products.UpdateStockById;

public class UpdateStockByIdCommand(int id , int stock) : IRequest
{
    public int Id { get; set; } = id;
    public int Stock { get; set; } = stock;
}
