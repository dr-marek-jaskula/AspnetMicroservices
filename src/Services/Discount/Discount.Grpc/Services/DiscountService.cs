using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Discount.Grpc.Mapping;

namespace Discount.Grpc.Services;

public sealed class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountService(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }

        return coupon.ToCouponModel();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.ToCoupon();
        await _discountRepository.CreateDiscount(coupon);
        return coupon.ToCouponModel();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.ToCoupon();
        await _discountRepository.UpdateDiscount(coupon);
        return coupon.ToCouponModel();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleteResult = await _discountRepository.DeleteDiscount(request.ProductName);
        var response = new DeleteDiscountResponse { Success = deleteResult };
        return response;
    }
}
