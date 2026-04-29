using CatFactsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatFactsApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatFactController : ControllerBase
{
    private readonly ICatFactService _catFactService;

    public CatFactController(ICatFactService catFactService)
    {
        _catFactService = catFactService;
    }

    [HttpGet("fetch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAndSaveFact()
    {
        try
        {
            var fact = await _catFactService.FetchAndSaveFactAsync();
            return Ok(new {Message = "Fact fetched and saved successfully", Data = fact});
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "External API is temporarily unavailable.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal server error occurred. Please try again later.");
        }
    }
}