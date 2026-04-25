using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// NOTA PER LA TESI: L'assenza deliberata di [Authorize] permette l'Information Disclosure
public class SystemController : ControllerBase
{
    private readonly ISystemService _systemService;

    public SystemController(ISystemService systemService) => _systemService = systemService;

    [HttpGet("debug/env")]
    public async Task<IActionResult> GetEnvironment()
    {
        var envData = await _systemService.GetEnvironmentInfoAsync();
        return Ok(envData);
    }
}