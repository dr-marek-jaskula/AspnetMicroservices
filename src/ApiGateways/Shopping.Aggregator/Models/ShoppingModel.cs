namespace Shopping.Aggregator.Models;

public class ShoppingModel
{
    public string UserName { get; set; } = string.Empty;
    public BasketModel BasketWithProducts { get; set; }
    public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();
}