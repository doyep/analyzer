namespace Doyep.Analyzer.Application.Strava;

/// <summary>
/// Defines a set of standardized error instances related to Strava API interactions,
/// providing consistent error codes, messages, and HTTP status codes for common failures.
/// </summary>
public static class StravaErrors
{
    /// <summary>
    /// Indicates that a request to the Strava API failed, which can occur due to network issues, API downtime, or other communication problems. This error is used to represent failures when exchanging authorization codes for tokens or when making authenticated requests to Strava's API. The error code is "strava.token_request_failed", the message is "Failed to communicate with Strava API.", and the HTTP status code is 502 (Bad Gateway) to indicate an upstream service failure.
    /// </summary>
    public static readonly Error TokenRequestFailed =
        new Error("strava.token_request_failed", "Failed to communicate with Strava API.", 502);

    public static readonly Error DeauthorizationFailed =
        new Error("strava.deauthorization_failed", "Failed to deauthorize Strava token.", 502);
}
