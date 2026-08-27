namespace Workbench.Modules.Authorization.Models;

/// <summary>
///     Interface for entities that are owned by a user.
/// </summary>
public interface IOwnedByUser
{
    /// <summary>
    ///     The ID of the user who owns the entity.
    /// </summary>
    int OwnerId { get; }
}
