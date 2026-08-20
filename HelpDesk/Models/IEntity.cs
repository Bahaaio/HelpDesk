namespace HelpDesk.Models;

/// <summary>
///     Base interface for all entities.
/// </summary>
/// <typeparam name="TKey">The type of the entity's identifier.</typeparam>
public interface IEntity<TKey>
{
    /// <summary>
    ///     The unique identifier of the entity.
    /// </summary>
    public TKey Id { get; }
}