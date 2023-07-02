using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;

//No inheritance from generated code because this app is just a client, not a server
public sealed class DiscountGrpcService 
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _client;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
    {
        _client = client;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequet = new GetDiscountRequest { ProductName = productName };
        return await _client.GetDiscountAsync(discountRequet);
    }
}
