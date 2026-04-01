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
        throw new NotImplementedException();
    }
}
