using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Examples;
using ProductService.Mediator.Products.CreateProduct;
using ProductService.Mediator.Products.GetProductById;
using ProductService.Mediator.Products.UpdateStockById;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ProductService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    [SwaggerOperation("Get Product By Id")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(GetProductExample))]
    [SwaggerResponse((int)HttpStatusCode.OK, "Product found", typeof(ProductDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Product not found")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id));

        if (product == null)
            return NotFound("Product Not Found!");

        return Ok(product);
    }

    [HttpPost]
    [SwaggerOperation("Create Product")]
    [SwaggerRequestExample(typeof(CreateProductCommand), typeof(CreateProductExample))]
    [SwaggerResponse((int)HttpStatusCode.OK, "Product created", typeof(ProductDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid product data")]
    public async Task<IActionResult> CreateProduct([FromBody]CreateProductCommand command)
    {
        var product = await mediator.Send(command);

        return Ok(product);
    }

    [HttpPut]
    [SwaggerOperation("Update Product Stock By Id")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Stock updated successfully")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid stock update data")]
    public async Task<IActionResult> UpdateProductById([FromBody]UpdateStockByIdCommand command)
    {
        await mediator.Send(command);
        return Ok("Updated Successfully");
    }
}
