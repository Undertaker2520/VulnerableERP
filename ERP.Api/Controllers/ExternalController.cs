using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExternalController : ControllerBase
{
    private readonly IExternalService _externalService;
    public ExternalController(IExternalService externalService) => _externalService = externalService;

    [HttpPost("proxy")]
    public async Task<IActionResult> Proxy([FromBody] SsrfRequest request)
    {
        var result = await _externalService.ProxyRequest(request.Url);
        return Ok(new { Content = result });
    }
}