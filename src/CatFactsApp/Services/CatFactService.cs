using System.Net.Http.Json;
using CatFactsApp.Models;

namespace CatFactsApp.Services;

public class CatFactService : ICatFactService
{
    private readonly HttpClient _httpClient;
    private readonly string _filePath;



    public CatFactService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _filePath = config["FileSettings:Path"] ?? "cat_facts.txt";
    }


    public async Task<CatFactDto> FetchAndSaveFactAsync()
    {

            var result = await _httpClient.GetFromJsonAsync<CatFactDto>("https://catfact.ninja/fact");
            
            if (result == null) 
            {
                throw new Exception("API did not return any data.");
            }

            await File.AppendAllTextAsync(_filePath, $"Fact: {result.Fact}{Environment.NewLine}Length: {result.Length}{Environment.NewLine}");

            return result;
    }
}