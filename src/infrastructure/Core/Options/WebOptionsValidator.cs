using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure;

/// <summary>
/// Validates the WebOptions to ensure all required fields are set and properly formatted.
/// </summary>
public class WebOptionsValidator : IValidateOptions<WebOptions>
{
    /// <summary>
    /// Validates the WebOptions instance. Checks for required fields and proper formatting, especially for the BaseUrl.
    /// </summary>
    public ValidateOptionsResult Validate(string? name, WebOptions options)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(options.BaseUrl))
            errors.Add("BaseUrl is required.");
        else if (!Uri.IsWellFormedUriString(options.BaseUrl, UriKind.Absolute))
            errors.Add("BaseUrl is not configured properly.");

        return errors.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(errors);
    }
}
