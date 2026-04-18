using Common.DTOs;
namespace BusinessLayer.Interfaces;

public interface ICouponService
{
    string ApplyCoupon(ApplyCouponRequest request);
}