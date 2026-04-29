using System.Net.Http.Json;
using CatFactsApp.Models;

namespace CatFactsApp.Services;

public class CatFactService : ICatFactService
{
    private readonly HttpClient _httpClient;



    public CatFactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<CatFactDto> FetchAndSaveFactAsync()
    {

            var result = await _httpClient.GetFromJsonAsync<CatFactDto>("https://catfact.ninja/fact");
            
            if (result == null) 
            {
                throw new Exception("API did not return any data.");
            }

            await File.AppendAllTextAsync("CatFacts.txt", $"Fact: {result.Fact}{Environment.NewLine}Length: {result.Length}{Environment.NewLine}");

            return result;
    }
}