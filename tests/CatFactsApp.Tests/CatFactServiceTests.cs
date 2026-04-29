using CatFactsApp.Services;
using CatFactsApp.Models;
using System.Net;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CatFactsApp.Tests;


public class CatFactServiceTests
{
    [Fact]
    public async Task FetchAndSaveFactAsync_ReturnsFact_WhenApiIsSuccessful()
    {
       // --- ARRANGE ---
        
        var handlerMock = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{\"fact\":\"Cats are liquid.\",\"length\":15}"),
        };
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        handlerMock
            .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(response);

        var httpClient = new HttpClient(handlerMock.Object);
        httpClient.BaseAddress = new Uri("https://api.test/"); 

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["FileSettings:Path"]).Returns("test_facts.txt");

        var loggerMock = new Mock<ILogger<CatFactService>>();

        var service = new CatFactService(httpClient, configMock.Object, loggerMock.Object);

        // --- ACT ---
        var result = await service.FetchAndSaveFactAsync();

        // --- ASSERT ---
        Assert.NotNull(result);
        Assert.Equal("Cats are liquid.", result.Fact);

        if (File.Exists("test_facts.txt")) File.Delete("test_facts.txt");
    }

    [Fact]
    public async Task FetchAndSaveFactAsync_ThrowsException_WhenApiReturnsNull()
    {
        // --- ARRANGE ---
        var handlerMock = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("null"), 
        };
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        handlerMock.Protected()
        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
        .ReturnsAsync(response);

        var loggerMock = new Mock<ILogger<CatFactService>>();
        var configMock = new Mock<IConfiguration>();

        var service = new CatFactService(
            new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://api.test/") }, 
            configMock.Object, 
            loggerMock.Object);

        // --- ACT & ASSERT ---
        var exception = await Assert.ThrowsAsync<Exception>(() => service.FetchAndSaveFactAsync());
        Assert.Equal("API did not return any data.", exception.Message);
    }
}