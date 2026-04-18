using Doyep.Analyzer.Api.Features.Auth;

namespace Doyep.Analyzer.Api;

/// <summary>
/// This class contains extension methods for registering API services to the DI container.
/// </summary>
public static class ApiServiceCollectionExtensions
{
    /// <summary>
    /// Register API services, such as the authentication state service, to the dependency injection container.
    /// </summary>
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddScoped<IAuthStateService, AuthStateService>();
        services.AddScoped<IAuthCookieService, AuthCookieService>();
        services.AddScoped<ICallbackValidator, CallbackValidator>();

        return services;
    }
}
