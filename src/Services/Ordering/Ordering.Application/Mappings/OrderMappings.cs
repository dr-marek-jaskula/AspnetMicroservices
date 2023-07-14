using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings;

public static class OrderMappings
{
    public static OrderVm ToVm(this Order order)
    {
        return new OrderVm
        {
            UserName = order.UserName,
            AddressLine = order.AddressLine,
            CardName = order.CardName,
            CardNumber = order.CardNumber,
            Country = order.Country,
            CVV = order.CVV,
            EmailAddress = order.EmailAddress,
            Expiration = order.Expiration,
            FirstName = order.FirstName,
            LastName = order.LastName,
            PaymentMethod = order.PaymentMethod,
            State = order.State,
            TotalPrice = order.TotalPrice,
            ZipCode = order.ZipCode
        };
    }

    public static Order ToEntity(this CheckoutOrderCommand command)
    {
        return new Order
        {
            UserName = command.UserName,
            AddressLine = command.AddressLine,
            CardName = command.CardName,
            CardNumber = command.CardNumber,
            Country = command.Country,
            CVV = command.CVV,
            EmailAddress = command.EmailAddress,
            Expiration = command.Expiration,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PaymentMethod = command.PaymentMethod,
            State = command.State,
            TotalPrice = command.TotalPrice,
            ZipCode = command.ZipCode
        };
    }
}