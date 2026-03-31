using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for generating JWT tokens for authenticated athletes.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for the specified athlete.
    /// It contains the following claims :
    /// <list type="bullet">
    ///   <item>
    ///     <description>The Strava ID</description>
    ///   </item>
    ///   <item>
    ///     <description>The role</description>
    ///   </item>
    ///   <item>
    ///     <description>The JWT token expiration date</description>
    ///   </item>
    /// </list>
    /// </summary>
    string Generate(Athlete athlete);
}
