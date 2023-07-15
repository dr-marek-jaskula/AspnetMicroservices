using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Basket.API.Mappings;
using MassTransit;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var basket = await _basketRepository.GetBasket(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpsertBasket([FromBody] ShoppingCart basket)
    {
        foreach (var item in basket.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }

        return Ok(await _basketRepository.UpsertBasket(basket));
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> DeleteBasket(string userName)
    {
        await _basketRepository.DeleteBasket(userName);
        return Ok();
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        var basket = await _basketRepository.GetBasket(basketCheckout.UserName);

        if (basket is null)
        {
            return BadRequest();
        }

        var eventMessage = basketCheckout.ToEvent(basket.TotalPrice);
        await _publishEndpoint.Publish(eventMessage);

        await _basketRepository.DeleteBasket(basket.UserName);
        return Accepted();
    }
}
