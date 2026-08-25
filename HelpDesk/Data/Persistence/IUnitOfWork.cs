namespace HelpDesk.Data.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}