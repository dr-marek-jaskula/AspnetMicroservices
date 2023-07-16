using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<BasketModel> GetBasket(string userName)
    {
        var response = await _httpClient.GetAsync($"/api/v1/Basket/{userName}");
        return await response.ReadContentAs<BasketModel>();
    }
}
