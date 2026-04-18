using BusinessLayer.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImportController : ControllerBase
{
    private readonly IImportService _importService;
    public ImportController(IImportService importService) => _importService = importService;

    [HttpPost("settings")]
    public IActionResult Import([FromBody] ImportRequest request)
    {
        var result = _importService.ImportSettings(request.JsonConfig);
        return Ok(new { Message = result });
    }
}