namespace Doyep.Analyzer.Domain;

/// <summary>
/// Provides extension methods for the <see cref="Role"/> enumeration.
/// </summary>
public static class RoleExtensions
{
    /// <summary>
    /// Returns a collection of roles that are inherited by the specified role.
    /// </summary>
    public static IEnumerable<Role> GetInheritedRoles(this Role role)
    {
        return role switch
        {
            Role.Admin => [Role.Admin, Role.User],
            Role.User => [Role.User],
            _ => Array.Empty<Role>()
        };
    }
}
