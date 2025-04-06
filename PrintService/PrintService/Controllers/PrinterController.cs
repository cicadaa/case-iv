using Microsoft.AspNetCore.Mvc;
using PrintService.Models;
using PrintService.Services;

namespace PrintService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PrintersController : ControllerBase
{
    private readonly IPrinterService _printerService;
    private readonly ILogger<PrintersController> _logger;

    public PrintersController(IPrinterService printerService, ILogger<PrintersController> logger)
    {
        _printerService = printerService;
        _logger = logger;
    }

    [HttpGet("{printerId?}")]
    [ProducesResponseType(typeof(IEnumerable<Printer>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetPrinters(string? printerId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(printerId))
            {
                var allPrinters = await _printerService.GetAllPrintersAsync();
                return Ok(allPrinters);
            }

            var printer = _printerService.GetPrinterAsync(printerId);
            return printer == null ? NotFound() : Ok(printer);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving printer data", ex);
            return StatusCode(500, new { message = "An unexpected error occurred while retrieving printer data." });
        }

    }
}


