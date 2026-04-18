using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;

    // Iniettiamo l'interfaccia del servizio, non più il DbContext!
    public CouponController(ICouponService couponService) => _couponService = couponService;

    [HttpPost("apply")]
    public IActionResult Apply([FromBody] ApplyCouponRequest request)
    {
        var result = _couponService.ApplyCoupon(request);
        return Ok(new { Message = result });
    }
}