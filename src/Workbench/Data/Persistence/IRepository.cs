using Workbench.Common.Exceptions;
using Workbench.Common.Models;

namespace Workbench.Data.Persistence;

/// <summary>
///     Interface for a generic repository.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
{
    /// <summary>
    ///     Retrieves an entity by its identifier or <c>null</c> if it does not exist.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <returns>The entity if found, otherwise <c>null</c>.</returns>
    Task<TEntity?> FindAsync(TKey id);

    /// <summary>
    ///     Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <exception cref="NotFoundException">Thrown when the entity with the specified key is not found.</exception>
    /// <returns>The entity if found.</returns>
    Task<TEntity> GetByIdAsync(TKey id);

    /// <summary>
    ///     Adds an entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    ///     Checks if the entity with a given id exists
    /// </summary>
    /// <param name="id">The id of the entity to check</param>
    /// <exception cref="NotFoundException">Thrown when the entity with the specified key is not found.</exception>
    Task ExistsOrThrowAsync(TKey id);

    /// <summary>
    ///     Updates an entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The updated entity.</returns>
    TEntity Update(TEntity entity);

    /// <summary>
    ///     Deletes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Remove(TEntity entity);
}
