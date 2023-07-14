using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Intrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Mappings;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public sealed class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand command, CancellationToken cancellationToken)
    {
        var orderEntity = command.ToEntity();
        var newOrder = await _orderRepository.AddAsync(orderEntity);
        _logger.LogInformation("Order {@NewOrderId} is successfully created.", newOrder.Id);
        await SendMail(newOrder);
        return newOrder.Id;
    }

    private async Task SendMail(Order order)
    {
        var email = new Email() { To = "eltinhh13@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Order {@NewOrderId} failed due to an error with the mail service: {@ExceptionMessage}", order.Id, ex.Message);
        }
    }
}