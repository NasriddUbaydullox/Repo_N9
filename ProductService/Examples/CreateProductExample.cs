using ProductService.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace ProductService.Examples;

public class CreateProductExample : IExamplesProvider<ProductDto>
{

    public ProductDto GetExamples()
    {
        return new ProductDto
        {
            Name = "iPhone",
            Stock = 20
        };
    }

}

public class GetProductExample : IExamplesProvider<ProductDto>
{
    public ProductDto GetExamples()
    {
        return new ProductDto
        {
            Id = 1,
            Name = "iPhone",
            Stock = 20
        };
    }
}
