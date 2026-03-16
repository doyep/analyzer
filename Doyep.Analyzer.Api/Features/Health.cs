namespace Doyep.Analyzer.Api;

/// <summary>
/// This class contains the endpoint for checking the health of the API.
/// </summary>
public static class Health
{
    public static IResult Handle()
    {
        return Results.Ok("Healthy");
    }
}
