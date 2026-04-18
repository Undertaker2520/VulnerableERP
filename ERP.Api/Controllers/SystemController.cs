using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//VULNERABILITA': enpoint non protetto con [Authorize] che espone dati sensibili 
public class SystemController : ControllerBase
{
    private readonly ISystemService _systemService;

    public SystemController(ISystemService systemService) => _systemService = systemService;

    [HttpGet("debug/env")]
    public IActionResult GetEnvironment()
    {
        var envData = _systemService.GetEnvironmentInfo();
        return Ok(envData);
    }
}