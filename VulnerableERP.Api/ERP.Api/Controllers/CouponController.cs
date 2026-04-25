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

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] ApplyCouponRequest request)
    {
        // Chiamata asincrona al servizio
        var result = await _couponService.ApplyCouponAsync(request);
        return Ok(new { Message = result });
    }
}