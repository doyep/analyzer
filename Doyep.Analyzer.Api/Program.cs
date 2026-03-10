using Doyep.Analyzer.Application.Strava;
using Doyep.Analyzer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .AddStrava(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/strava/token/{code}", async (string code, IStravaAuthenticationService strava) =>
{
    return await strava.ExchangeToken(code);
});

app.Run();
