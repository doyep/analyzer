using Scalar.AspNetCore;

using Doyep.Analyzer.Infrastructure;
using Doyep.Analyzer.Api;
using Doyep.Analyzer.Infrastructure.Strava;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOptions<StravaApplicationOptions>()
    .BindConfiguration(StravaApplicationOptions.SectionName)
    .ValidateOnStart();
builder.Services
    .AddOpenApi()
    .AddStrava();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar")).ExcludeFromDescription();
}

app.MapApiEndpoints();
app.MapAuthEndpoints();

app.UseHttpsRedirection();

app.Run();
