using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductService.Dtos;
using ProductService.Reposiotries.Interfaces;

namespace ProductService.Mediator.Products.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository repo) : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repo.GetByIdAsync(request.id);

        if (product == null)
            throw new Exception("Product Not Found");

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Stock = product.Stock
        };
    }
}
