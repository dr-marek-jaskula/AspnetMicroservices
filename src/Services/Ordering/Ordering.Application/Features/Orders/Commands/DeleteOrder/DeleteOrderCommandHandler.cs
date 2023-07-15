using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderRepository.GetByIdAsync(command.Id);

        if (orderToDelete is null)
        {
            _logger.LogError("Order not found.");
            throw new NotFoundException(nameof(Order), command.Id);
        }

        await _orderRepository.DeleteAsync(orderToDelete);
    }
}