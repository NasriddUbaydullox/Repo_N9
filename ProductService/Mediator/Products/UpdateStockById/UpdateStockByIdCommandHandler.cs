using MediatR;
using ProductService.Reposiotries.Interfaces;

namespace ProductService.Mediator.Products.UpdateStockById;

public class UpdateStockByIdCommandHandler(IProductRepository repo) : IRequestHandler<UpdateStockByIdCommand>
{
    public async Task Handle(UpdateStockByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await repo.GetByIdAsync(request.Id);

        if (product == null)
            throw new Exception("Product Not Found!");

        product.Stock = request.Stock;

        await repo.SaveChangesAsync();
    }
}
