namespace Workbench.Data.Persistence.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
