using MediatR;
using MassTransit;
using Ordering.API.Mappings;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumers;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly ISender _sender;
    private readonly ILogger<BasketCheckoutConsumer> _logger;
    public BasketCheckoutConsumer(ISender sender, ILogger<BasketCheckoutConsumer> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        CheckoutOrderCommand command = context.Message.ToCheckoutOrderCommand();
        var result = await _sender.Send(command);
        _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id: {newOrderId}", result);
    }
}
