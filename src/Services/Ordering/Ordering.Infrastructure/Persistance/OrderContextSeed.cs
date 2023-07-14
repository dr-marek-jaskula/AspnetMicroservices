using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance;

public sealed class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (orderContext.Orders.Any() is false)
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new Order() {UserName = "swn", FirstName = "Marek", LastName = "Jaskula", EmailAddress = "someEmail@gmail.com", AddressLine = "Lodz", Country = "Poland", TotalPrice = 350 }
        };
    }
}