using MediatR;
using ProductService.DataAccess.Entities;
using ProductService.Dtos;
using ProductService.Reposiotries.Interfaces;

namespace ProductService.Mediator.Products.CreateProduct;

public class CreateProductCommandHandler(IProductRepository repo) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.dto.Name,
            Stock = request.dto.Stock,
        };

        await repo.AddAsync(product);

        return product.Id;
    }
}
