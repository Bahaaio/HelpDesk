namespace HelpDesk.Modules.Attachments.Models;

/// <summary>
///     Interface for entities that have a parent entity.
/// </summary>
public interface IHasParent
{
    /// <summary>
    ///     The ID of the parent entity.
    /// </summary>
    public int ParentId { get; set; }
}

/// <inheritdoc cref="IHasParent" />
/// <typeparam name="TParent">The type of the parent entity.</typeparam>
public interface IHasParent<TParent> : IHasParent
    where TParent : class
{
}