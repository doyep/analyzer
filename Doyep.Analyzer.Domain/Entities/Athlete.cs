namespace Doyep.Analyzer.Domain;

/// <summary>
/// Represents an athlete within the application.
/// </summary>
public class Athlete
{
    /// <summary>
    /// The unique identifier for the athlete, corresponding to their Strava account ID.
    /// </summary>
    public long StravaId { get; init; }

    /// <summary>
    /// The first name of the athlete. It can be null if the athlete has been pre-authorized with only their Strava ID.
    /// </summary>
    public string? FirstName { get; private set; }

    /// <summary>
    /// The last name of the athlete. It can be null if the athlete has been pre-authorized with only their Strava ID.
    /// </summary>
    public string? LastName { get; private set; }

    /// <summary>
    /// The role of the athlete within the application, determining their access level and permissions.
    /// </summary>
    public Role Role { get; private set; }

    /// <summary>
    /// The timestamp of the athlete's last connection to the application. It can be null if the athlete has never connected or has been pre-authorized.
    /// </summary>
    public DateTimeOffset? LastConnection { get; private set; }

    /// <summary>
    /// Determines if the athlete has access rights to the application based on their role.
    /// </summary>
    public bool HasAccess()
    {
        return Role != Role.None;
    }

    /// <summary>
    /// Grants access to the athlete if they have no access rights.
    /// </summary>
    public void GrantAccess()
    {
        if (Role == Role.None) Role = Role.User;
    }

    /// <summary>
    /// Revokes access from the athlete, setting their role to None regardless of their current role.
    /// </summary>
    public void RevokeAccess()
    {
        Role = Role.None;
    }

    /// <summary>
    /// Updates the athlete's first and last name, and sets the last connection timestamp.
    /// </summary>
    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        LastConnection = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Factory method to create a new Athlete instance with the provided Strava ID, first name, and last name. The new athlete will have no access rights and the last connection timestamp will be set to the current time.
    /// </summary>
    public static Athlete Register(long stravaId, string firstName, string lastName)
    {
        return new Athlete
        {
            StravaId = stravaId,
            FirstName = firstName,
            LastName = lastName,
            Role = Role.None,
            LastConnection = DateTimeOffset.UtcNow,
        };
    }

    /// <summary>
    /// Factory method to create a new Athlete instance with the provided Strava ID. This method is intended for pre-authorizing users based on their Strava ID before they have connected to the application.
    /// </summary>
    public static Athlete PreAuthorize(long stravaId)
    {
        return new Athlete
        {
            StravaId = stravaId,
            Role = Role.User,
        };
    }
}
