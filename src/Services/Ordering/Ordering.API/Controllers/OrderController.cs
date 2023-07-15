using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System.Net;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class OrderController : ControllerBase
{
    private readonly ISender _sender;

    public OrderController(IMediator mediator)
    {
        _sender = mediator;
    }

    [HttpGet("{userName}", Name = "GetOrder")]
    [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrdersByUserName(string userName)
    {
        var query = new GetOrdersListQuery(userName);
        var orders = await _sender.Send(query);
        return Ok(orders);
    }

    //Test purpose, proper approach is in BasketCheckoutConsumer
    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var command = new DeleteOrderCommand() { Id = id };
        await _sender.Send(command);
        return NoContent();
    }
}
