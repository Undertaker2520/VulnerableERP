using BusinessLayer.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// Ricorda: l'assenza di [Authorize] qui è una scelta di design
public class ImportController : ControllerBase
{
    private readonly IImportService _importService;
    public ImportController(IImportService importService) => _importService = importService;

    [HttpPost("settings")]
    public async Task<IActionResult> Import([FromBody] ImportRequest request)
    {
        var result = await _importService.ImportSettingsAsync(request.JsonConfig);
        return Ok(new { Message = result });
    }
}