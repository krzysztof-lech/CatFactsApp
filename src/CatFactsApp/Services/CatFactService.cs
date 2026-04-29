using System.Net.Http.Json;
using CatFactsApp.Models;

namespace CatFactsApp.Services;

public class CatFactService : ICatFactService
{
    private readonly HttpClient _httpClient;
    private readonly string _filePath;
    private readonly ILogger<CatFactService> _logger;



    public CatFactService(HttpClient httpClient, IConfiguration config, ILogger<CatFactService> logger)
    {
        _httpClient = httpClient;
        _filePath = config["FileSettings:Path"] ?? "cat_facts.txt";
        _logger = logger;
    }


    public async Task<CatFactDto> FetchAndSaveFactAsync()
    {
        try
        {
             _logger.LogInformation("Starting to fetch a fact from the API.");
            var result = await _httpClient.GetFromJsonAsync<CatFactDto>("fact");
            
            if (result == null) 
            {
                _logger.LogWarning("API returned null data.");
                throw new Exception("API did not return any data.");
            }

            await File.AppendAllTextAsync(_filePath, $"Fact: {result.Fact}{Environment.NewLine}Length: {result.Length}{Environment.NewLine}");

            return result;
        }catch(OperationCanceledException)
        {
            _logger.LogWarning("The operation was canceled.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching or saving fact.");
            throw;
        }
           
    }
}