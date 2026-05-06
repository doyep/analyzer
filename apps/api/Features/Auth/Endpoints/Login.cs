using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Security;

namespace Doyep.Analyzer.Api;

/// <summary>
/// Handles the user login process by generating a Strava login URL.
/// </summary>
public static class Login
{
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/login", (
            string deviceId,
            string redirectUri,
            HttpContext context,
            ILoginService loginService,
            IStateService stateService) =>
        {
            var stateResult = ValidateQueryParametersAndCreateState(stateService, deviceId, redirectUri);
            if (stateResult.IsFailure)
                return Results.BadRequest(new { error = stateResult.Error.Message });

            var callbackUri = new UriBuilder
            {
                Scheme = context.Request.Scheme,
                Host = context.Request.Host.Host,
                Port = context.Request.Host.Port ?? -1,
                Path = "/auth/callback"
            }.Uri;

            var loginUrl = loginService.GenerateStravaLoginUrl(stateResult.Value, callbackUri);

            return Results.Ok(new { url = loginUrl });
        })
            .WithDescription("Generates a Strava login URL for the user to initiate the authentication process.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }

    /// <summary>
    /// Validates the query parameters for the login endpoint and creates a state parameter for the Strava login URL.
    /// This method checks if the device ID is a valid GUID and if the redirect URI is a valid absolute URI. If either
    /// validation fails, it returns an error result with an appropriate message and status code. If both validations succeed,
    /// it creates a state parameter using the provided state service and returns it as a success result.
    /// </summary>
    /// <param name="stateService" >The state service used to create the state parameter.</param>
    /// <param name="deviceId">The device ID provided in the query parameters.</param>
    /// <param name="redirectUri">The redirect URI provided in the query parameters.</param>
    /// <returns>A result containing either the state parameter or an error.</returns>
    private static Result<string, Error> ValidateQueryParametersAndCreateState(IStateService stateService, string deviceId, string redirectUri)
    {
        if (!Guid.TryParse(deviceId, out var deviceIdParsed))
            return Result<string, Error>.Failure(new Error("login.invalid_device_id", "Invalid device ID format.", StatusCodes.Status400BadRequest));

        if (!Uri.TryCreate(redirectUri, UriKind.Absolute, out var redirectUriParsed))
            return Result<string, Error>.Failure(new Error("login.invalid_redirect_uri", "Invalid redirect URI format.", StatusCodes.Status400BadRequest));

        var state = stateService.Create(deviceIdParsed, redirectUriParsed);

        return Result<string, Error>.Success(state);
    }
}
