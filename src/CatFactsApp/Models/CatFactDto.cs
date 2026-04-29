using System.Text.Json.Serialization;

namespace CatFactsApp.Models;

public class CatFactDto
{
    [JsonPropertyName("fact")]
    public string Fact { get; set; } = string.Empty;
    [JsonPropertyName("length")]
    public int Length { get; set; }
}