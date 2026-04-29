using CatFactsApp.Models;

namespace CatFactsApp.Services;

public interface ICatFactService
{
    Task<CatFactDto> FetchAndSaveAsync();
}