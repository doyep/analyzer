using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Infrastructure.Strava;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .Configure<StravaOptions>(builder.Configuration.GetSection("Strava"))
    .AddHttpClient<IStravaService, StravaService>(client =>
    {
        client.BaseAddress = new Uri("https://www.strava.com/api/v3");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/strava/token/{code}", async (string code, IStravaService strava) =>
{
    return await strava.ExchangeToken(code);
});


app.Run();
