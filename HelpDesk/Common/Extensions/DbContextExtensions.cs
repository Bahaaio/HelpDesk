using HelpDesk.Common.Exceptions;
using HelpDesk.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Common.Extensions;

/// <summary>
///     Extension methods for <see cref="DbSet{TEntity}" />.
/// </summary>
public static class DbContextExtensions
{
    /// <param name="set">The <see cref="DbSet{TEntity}" /> to search in.</param>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    extension<TEntity, TKey>(DbSet<TEntity> set)
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        /// <summary>
        ///     Finds an entity by its primary key value.
        /// </summary>
        /// <param name="key">The primary key value of the entity to find.</param>
        /// <returns>The entity if found.</returns>
        /// <exception cref="NotFoundException">Thrown when the entity with the specified key is not found.</exception>
        public async Task<TEntity> FindOrThrowAsync(TKey key)
        {
            var entity = await set.FindAsync(key);

            if (entity is null)
                throw new NotFoundException($"Resource with id {key} not found");

            return entity;
        }

        /// <summary>
        ///     Checks if an entity with the specified primary key value exists.
        /// </summary>
        /// <param name="key">The primary key value of the entity to check.</param>
        /// <exception cref="NotFoundException">Thrown when the entity with the specified key does not exist.</exception>
        public async Task ExistsOrThrowAsync(TKey key)
        {
            var exists = await set.AnyAsync(e => e.Id.Equals(key));

            if (!exists)
                throw new NotFoundException($"Resource with id {key} not found");
        }
    }
}