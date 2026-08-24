namespace HelpDesk.Modules.Attachments.Models;

/// <summary>
///     Interface for entities that have a parent entity.
/// </summary>
/// <typeparam name="TParent">The type of the parent entity.</typeparam>
public interface IHasParent<TParent>
    where TParent : class
{
    /// <summary>
    ///     The ID of the parent entity.
    /// </summary>
    public int ParentId { get; set; }
}