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
    .AddJwtAuthentication()
    .AddReverseProxy(builder.Configuration);

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
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();
app.MapApiEndpoints();

app.MapReverseProxy();

app.Run();
