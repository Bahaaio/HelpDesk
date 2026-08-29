using Workbench.Modules.Milestones.Dtos;
using Workbench.Modules.Milestones.Dtos.Requests;

namespace Workbench.Modules.Milestones.Services;

public interface IMilestonesService
{
    /// <summary>Returns all milestones for a project.</summary>
    Task<List<MilestoneDto>> GetAll(int projectId);

    /// <summary>Returns a single milestone by ID.</summary>
    Task<MilestoneDto> GetById(int projectId, int milestoneId);

    /// <summary>Creates a new milestone in the project.</summary>
    Task<MilestoneDto> Create(int projectId, CreateMilestoneRequest request);

    /// <summary>Updates an existing milestone.</summary>
    Task<MilestoneDto> Update(int projectId, int milestoneId, UpdateMilestoneRequest request);

    /// <summary>Deletes a milestone.</summary>
    Task Delete(int projectId, int milestoneId);
}
