using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Utility class to validate the list of allowed redirect hosts for authentication flows.
/// This is used to ensure that the "redirect_uri" parameter in authentication requests is
/// validated against a list of allowed hosts to prevent open redirect vulnerabilities.
/// </summary>
public class RedirectHostsValidator(
    IOptions<AllowedRedirectHostsOptions> allowedRedirectHostsOptions
)
{
    private readonly string[] _allowedRedirectHosts = allowedRedirectHostsOptions.Value.Hosts.Split(',', StringSplitOptions.RemoveEmptyEntries);

    private readonly string[] defautlAllowedHosts = [
        "localhost",
        "127.0.0.1",
        "[::1]"
    ];

    public bool IsAllowedHost(string host)
    {
        return _allowedRedirectHosts.Any(allowedHost => string.Equals(host, allowedHost, StringComparison.OrdinalIgnoreCase))
            || defautlAllowedHosts.Any(allowedHost => string.Equals(host, allowedHost, StringComparison.OrdinalIgnoreCase));
    }
}

