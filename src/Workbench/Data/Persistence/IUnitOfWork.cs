namespace Workbench.Data.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
