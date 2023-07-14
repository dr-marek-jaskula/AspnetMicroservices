using MediatR;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Mappings;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public sealed class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderVm>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderVm>> Handle(GetOrdersListQuery query, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetOrdersByUserName(query.UserName);

        return orderList
            .Select(x => x.ToVm())
            .ToList();
    }
}