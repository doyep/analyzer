namespace Doyep.Analyzer.Application.Strava;

public interface IStravaAuthenticationService
{
    Task<string> ExchangeToken(string authorizationCode);
}
