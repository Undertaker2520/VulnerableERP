using BusinessLayer.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService) => _couponService = couponService;

    [HttpPost("validate")]
    public async Task<IActionResult> Validate([FromBody] ApplyCouponRequest request)
    {
        var result = await _couponService.ValidateCouponAsync(request);
        if (result != "OK") return BadRequest(new { Message = result });
        return Ok(new { Message = "OK" });
    }

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] ApplyCouponRequest request)
    {
        var result = await _couponService.ApplyCouponAsync(request);
        return Ok(new { Message = result });
    }
}