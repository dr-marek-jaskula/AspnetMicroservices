using Basket.API.Entities;
using EventBus.Messages.Events;

namespace Basket.API.Mappings;

public static class BasketMapping
{
    public static BasketCheckoutEvent ToEvent(this BasketCheckout basket, decimal totalPrice)
    {
        return new BasketCheckoutEvent()
        {
            AddressLine = basket.AddressLine,
            CardName = basket.CardName,
            CardNumber = basket.CardNumber,
            Country = basket.Country,
            CVV = basket.CVV,
            EmailAddress = basket.EmailAddress,
            Expiration = basket.Expiration,
            FirstName = basket.FirstName,
            LastName = basket.LastName,
            PaymentMethod = basket.PaymentMethod,
            State = basket.State,
            TotalPrice = totalPrice,
            UserName = basket.UserName,
            ZipCode = basket.ZipCode,
        };
    }
}
