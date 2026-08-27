using Workbench.Common.Exceptions;
using Workbench.Common.Extensions;
using Workbench.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Workbench.Data.Persistence.Implementations;

/// <summary>
///     Generic repository implementation.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
{
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(AppDbContext context)
    {
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> FindAsync(TKey id) => await DbSet.FindAsync(id);

    public virtual async Task<TEntity> GetByIdAsync(TKey id) =>
        await DbSet.FindAsync(id) ??
        throw new NotFoundException($"Resource with id {id} not found");

    public TEntity Add(TEntity entity) => DbSet.Add(entity).Entity;

    public Task ExistsOrThrowAsync(TKey id) => DbSet.ExistsOrThrowAsync(id);

    public TEntity Update(TEntity entity) => DbSet.Update(entity).Entity;

    public void Remove(TEntity entity) => DbSet.Remove(entity);
}
