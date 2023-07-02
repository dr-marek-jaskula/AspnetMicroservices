using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Discount.Grpc.Mapping;

namespace Discount.Grpc.Services;

public sealed class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
    {
        _discountRepository = discountRepository;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }

        _logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);
        return coupon.ToCouponModel();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.ToCoupon();
        await _discountRepository.CreateDiscount(coupon);
        _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);
        return coupon.ToCouponModel();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.ToCoupon();
        await _discountRepository.UpdateDiscount(coupon);
        _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
        return coupon.ToCouponModel();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleteResult = await _discountRepository.DeleteDiscount(request.ProductName);
        var response = new DeleteDiscountResponse { Success = deleteResult };
        return response;
    }
}
