using Doyep.Analyzer.Api;
using Doyep.Analyzer.Infrastructure;
using Doyep.Analyzer.Infrastructure.Persistence;
using Doyep.Analyzer.Infrastructure.Strava;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOptions<StravaApplicationOptions>()
    .BindConfiguration(StravaApplicationOptions.SectionName)
    .ValidateOnStart();
builder.Services.AddStrava();

builder.Services
    .AddOptions<AnalyzerDbContextOptions>()
    .BindConfiguration(AnalyzerDbContextOptions.SectionName)
    .ValidateOnStart();
builder.Services.AddPersistence();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar")).ExcludeFromApiReference();
}

app.MapApiEndpoints();
app.MapAuthEndpoints();

app.UseHttpsRedirection();

app.Run();