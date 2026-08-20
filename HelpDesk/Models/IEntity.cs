namespace HelpDesk.Models;

/// <summary>
///     Base interface for all entities.
/// </summary>
/// <typeparam name="TKey">The type of the entity's identifier.</typeparam>
public interface IEntity<TKey>
{
    public TKey Id { get; }
}