using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.DataAccess.Entities;
using OrderService.Dtos;
using OrderService.Examples;
using OrderService.Mediator.Order.CreateOrder;
using OrderService.Mediator.Order.GetOrders;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}")]
    [SwaggerOperation("Get Product By Id")]
    [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(GetOrderExample))]
    [SwaggerResponse((int)HttpStatusCode.OK, "Product found", typeof(OrderDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Product not found")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await mediator.Send(new GetOrderByIdQuery(id));

        if(order == null) 
            return NotFound("Order Not Found!");

        return Ok(order);
    }

    [HttpPost]
    [SwaggerOperation("Create Product")]
    [SwaggerRequestExample(typeof(CreateOrderCommand), typeof(CreateOrderExample))]
    [SwaggerResponse((int)HttpStatusCode.OK, "Product created", typeof(OrderDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid product data")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var order = await mediator.Send(command);
        return Ok(order);
    }
}
