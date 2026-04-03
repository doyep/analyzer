namespace Doyep.Analyzer.Domain;

/// <summary>
/// Enumeration representing user roles in the application. 
/// </summary>
public enum Role
{
    /// <summary>
    /// Represents a user with no access rights.
    /// </summary>
    None,

    /// <summary>
    /// Represents a regular user.
    /// </summary>
    User,

    /// <summary>
    /// Represents an administrator that can manage users and synchronizations.
    /// </summary>
    Admin,
}
