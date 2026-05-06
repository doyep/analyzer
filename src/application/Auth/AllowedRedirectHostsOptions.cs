namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Configuration options for allowed redirect hosts in authentication flows.
/// These values are loaded from the "AllowedRedirectHosts" section of appsettings.json,
/// from environment variables or user secrets.
/// </summary>
public class AllowedRedirectHostsOptions
{
    /// <summary>
    /// Section name of appsettings.json
    /// </summary>
    public const string SectionName = "Auth:AllowedRedirectHosts";

    /// <summary>
    /// A comma-separated list of allowed redirect hosts for authentication flows.
    /// This value is used to validate the "redirect_uri" parameter in authentication requests to prevent open redirect vulnerabilities.
    /// Each host should be specified in the format "scheme://host:port" (e.g., "https://example.com:443").
    /// </summary>
    public required string Hosts { get; set; }
}
