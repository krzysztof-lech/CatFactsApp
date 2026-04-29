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
    public async Task<IActionResult> GetAndSaveFact()
    {
        try
        {
            var fact = await _catFactService.FetchAndSaveFactAsync();
            return Ok(new {Message = "Fact fetched and saved successfully", Data = fact});
        }
        catch(Exception e)
        {
            return BadRequest(new { Error = e.Message});
        }
    }
}