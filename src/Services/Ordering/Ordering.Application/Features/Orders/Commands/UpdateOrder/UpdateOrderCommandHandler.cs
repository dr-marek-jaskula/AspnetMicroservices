using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Intrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetByIdAsync(command.Id);

        if (orderToUpdate is null)
        {
            _logger.LogError("Order not found.");
            throw new NotFoundException(nameof(Order), command.Id);
        }

        UpdateOrder(orderToUpdate, command);

        await _orderRepository.UpdateAsync(orderToUpdate);
    }

    public static void UpdateOrder(Order order, UpdateOrderCommand command)
    {
        order.UserName = command.UserName;
        order.AddressLine = command.AddressLine;
        order.CardName = command.CardName;
        order.CardNumber = command.CardNumber;
        order.Country = command.Country;
        order.CVV = command.CVV;
        order.EmailAddress = command.EmailAddress;
        order.Expiration = command.Expiration;
        order.FirstName = command.FirstName;
        order.LastName = command.LastName;
        order.PaymentMethod = command.PaymentMethod;
        order.State = command.State;
        order.TotalPrice = command.TotalPrice;
        order.ZipCode = command.ZipCode;
    }
}