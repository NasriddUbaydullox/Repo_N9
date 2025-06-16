using MediatR;
using ProductService.Dtos;

namespace ProductService.Mediator.Products.CreateProduct;

public record CreateProductCommand(CreateProductDto dto) : IRequest<int>;
