using CatFactsApp.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddHttpClient<ICatFactService, CatFactService>(c =>
{
    var baseUrl = builder.Configuration["FileSettings:BaseUrl"];
    
    if (string.IsNullOrEmpty(baseUrl)) 
    {
        throw new Exception("Configuration error: 'BaseUrl' is missing in 'FileSettings' section of appsettings.json.");
    }

    c.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
