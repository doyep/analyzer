using Doyep.Analyzer.Api;
using Doyep.Analyzer.Api.Features.Auth;
using Doyep.Analyzer.Application;
using Doyep.Analyzer.Infrastructure;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure()
    .AddJwtAuthentication(builder.Configuration);

// TODO : Properly configure Policies
builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("admin", policy => policy.RequireRole("Admin"))
    .AddPolicy("user", policy => policy.RequireRole("User"));

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar")).ExcludeFromApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints();
app.MapAuthEndpoints();

app.UseHttpsRedirection();

app.Run();
