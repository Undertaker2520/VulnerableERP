using Common.DTOs;

namespace BusinessLayer.Interfaces;

public interface ICouponService
{
    Task<string> ApplyCouponAsync(ApplyCouponRequest request);
    Task<string> ValidateCouponAsync(ApplyCouponRequest request);
}