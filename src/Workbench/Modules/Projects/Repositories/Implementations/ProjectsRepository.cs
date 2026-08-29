using Microsoft.EntityFrameworkCore;
using Workbench.Common.Exceptions;
using Workbench.Data;
using Workbench.Data.Persistence.Implementations;
using Workbench.Modules.Projects.Dtos;
using Workbench.Modules.Projects.Mappers;
using Workbench.Modules.Projects.Models;

namespace Workbench.Modules.Projects.Repositories.Implementations;

public class ProjectsRepository : Repository<Project, int>, IProjectsRepository
{
    private readonly AppDbContext _context;

    public ProjectsRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Project> GetByIdAsync(int id) =>
        await DbSet
            .Include(p => p.Owner)
            .SingleOrDefaultAsync(p => p.Id == id)
        ?? throw new NotFoundException($"Project with id {id} not found");

    public Task<List<ProjectDto>> GetAllAsync() =>
        DbSet.Select(ProjectMapper.ToDtoExpression).ToListAsync();

    public Task<List<ProjectDto>> GetAllByUserIdAsync(int userId) =>
        DbSet
            .Where(p => p.OwnerId == userId)
            .Select(ProjectMapper.ToDtoExpression)
            .ToListAsync();

    public Task LoadOwnerAsync(Project project) =>
        _context.Entry(project).Reference(p => p.Owner).LoadAsync();
}