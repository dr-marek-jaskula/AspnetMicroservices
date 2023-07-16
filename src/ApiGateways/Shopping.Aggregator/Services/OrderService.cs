using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
    {
        var response = await _httpClient.GetAsync($"/api/v1/Order/{userName}");
        return await response.ReadContentAs<List<OrderResponseModel>>();
    }
}
