using MediatR;
using ProductService.Dtos;

namespace ProductService.Mediator.Products.GetProductById;

public record GetProductByIdQuery(int id) : IRequest<ProductDto>;
