using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderCommand : IRequest
{
    public int Id { get; set; }
}