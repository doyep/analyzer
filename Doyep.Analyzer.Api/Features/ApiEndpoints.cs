using Scalar.AspNetCore;

namespace Doyep.Analyzer.Api;

/// <summary>
/// This class contains extension methods for mapping API endpoints to the application.
/// </summary>
public static class ApiEndpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        var apiGroup = app.MapGroup("/api");

        apiGroup.MapGet("/health", Health.Handle)
            .ExcludeFromApiReference();

        return app;
    }
}