namespace Doyep.Analyzer.Application.Strava;

public interface IStravaService
{
    Task<string> ExchangeToken(string authorizationCode);
}
