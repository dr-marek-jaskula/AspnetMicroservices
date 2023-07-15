using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mappings;

public static class EventMappings
{
    public static CheckoutOrderCommand ToCheckoutOrderCommand(this BasketCheckoutEvent basketCheckoutEvent)
    {
        return new CheckoutOrderCommand()
        {
            AddressLine = basketCheckoutEvent.AddressLine,
            CardName = basketCheckoutEvent.CardName,
            CardNumber = basketCheckoutEvent.CardNumber,
            Country = basketCheckoutEvent.Country,
            CVV = basketCheckoutEvent.CVV,
            EmailAddress = basketCheckoutEvent.EmailAddress,
            Expiration = basketCheckoutEvent.Expiration,
            FirstName = basketCheckoutEvent.FirstName,
            LastName = basketCheckoutEvent.LastName,
            PaymentMethod = basketCheckoutEvent.PaymentMethod,
            State = basketCheckoutEvent.State,
            TotalPrice = basketCheckoutEvent.TotalPrice,
            UserName = basketCheckoutEvent.UserName,
            ZipCode = basketCheckoutEvent.ZipCode
        };
    }
}
