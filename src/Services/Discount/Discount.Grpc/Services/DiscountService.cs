using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountContext dbContext,ILogger<DiscountService> logger)
    :DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        //TODO : GetDiscunt from Database
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon is null)
            coupon = new Coupon { ProductName="No Discount", Amount =0,Description="No Discount Description"};
        var couponModal = coupon.Adapt<CouponModel>();
        logger.LogInformation("Discount is retrived for ProductName : {productName},Amount : {amount},Description : {description}", coupon.ProductName, coupon.Amount, coupon.Description);
        return couponModal;
    }
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

          dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully created for ProductName : {productName},Amount : {amount},Description : {description}", coupon.ProductName, coupon.Amount, coupon.Description);
        var couponModal = coupon.Adapt<CouponModel>();
        return couponModal;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully updated for ProductName : {productName},Amount : {amount},Description : {description}", coupon.ProductName, coupon.Amount, coupon.Description);
        var couponModal = coupon.Adapt<CouponModel>();
        return couponModal;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName = {request.ProductName} is not found "));
        
        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        return new DeleteDiscountResponse { Success = true };


    }
}
