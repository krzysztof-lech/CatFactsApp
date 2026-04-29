using CatFactsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatFactsApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatFactController : ControllerBase
{
    private readonly ICatFactService _catFactService;
    private readonly ILogger<CatFactController> _logger;

    public CatFactController(ICatFactService catFactService, ILogger<CatFactController> logger)
    {
        _catFactService = catFactService;
        _logger = logger;
    }

    [HttpGet("fetch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAndSaveFact(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Attempting to fetch a cat fact.");
            var fact = await _catFactService.FetchAndSaveFactAsync(ct);
            return Ok(new {Message = "Fact fetched and saved successfully", Data = fact});
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Network error occurred.");
            return StatusCode(503, "External API is temporarily unavailable.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            return StatusCode(500, "An internal server error occurred. Please try again later.");
        }
    }
}