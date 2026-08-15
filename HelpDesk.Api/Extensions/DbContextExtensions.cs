using HelpDesk.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Extensions;

public static class DbContextExtensions
{
    extension<TEntity>(DbSet<TEntity> set) where TEntity : class
    {
        /// <summary>
        ///     Finds an entity by its primary key value.
        /// </summary>
        /// <param name="key">The primary key value of the entity to find.</param>
        /// <returns>The entity if found.</returns>
        /// <exception cref="NotFoundException">Thrown when the entity with the specified key is not found.</exception>
        public async Task<TEntity> FindOrThrowAsync(object key)
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
        public async Task ExistsOrThrowAsync(object key)
        {
            var pkName = set.EntityType.FindPrimaryKey()!.Properties[0].Name;
            var exists = await set.AnyAsync(e => EF.Property<object>(e, pkName).Equals(key));

            if (!exists)
                throw new NotFoundException($"Resource with id {key} not found");
        }
    }
}