using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public sealed class GetOrdersListQuery : IRequest<List<OrderVm>>
{
    public string UserName { get; set; }

    public GetOrdersListQuery(string userName)
    {
        UserName = userName;
    }


}