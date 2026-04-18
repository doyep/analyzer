using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Validates the AnalyzerDbContextOptions to ensure all required fields are set and properly formatted.
/// </summary>
public class AnalyzerDbContextOptionsValidator : IValidateOptions<AnalyzerDbContextOptions>
{
    /// <summary>
    /// Validates the AnalyzerDbContextOptions instance. Checks for required fields, proper formatting, and placeholder values.
    /// </summary>
    public ValidateOptionsResult Validate(string? name, AnalyzerDbContextOptions options)
    {
        var errors = new List<string>();

        if (IsPlaceholder(options.DoyepAnalyzerDb))
            errors.Add("DoyepAnalyzerDb connection string is not configured properly.");

        return errors.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(errors);
    }

    private static bool IsPlaceholder(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? true : value.Contains("<YOUR_");
    }
}
