namespace Doyep.Analyzer.Infrastructure.Security;

public class StateOptions
{
    /// <summary>
    /// Section name of appsettings.json
    /// </summary>
    public const string SectionName = "Auth:State";

    /// <summary>
    /// The expiration time of the state token in minutes. This value determines how long the token is valid before it expires and can no longer be used for authentication.
    /// </summary>
    public required int ExpirationInMinutes { get; set; }
}
