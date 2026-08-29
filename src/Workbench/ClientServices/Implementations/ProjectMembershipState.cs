using Workbench.Modules.Projects.Enums;
using Workbench.Modules.Projects.Memberships.Services;

namespace Workbench.ClientServices.Implementations;

public class ProjectMembershipState : IProjectMembershipState
{
    private readonly IProjectMembershipsService _membershipsService;

    public ProjectMembershipState(IProjectMembershipsService membershipsService)
    {
        _membershipsService = membershipsService;
    }

    public bool IsLoaded { get; private set; }
    public bool IsMember { get; private set; }
    public bool IsLead { get; private set; }
    public bool IsOwner { get; private set; }
    public ProjectMemberRole? Role { get; private set; }

    public async Task Load(int projectId)
    {
        var membership = await _membershipsService.GetCurrentUserProjectMembership(projectId);
        IsMember = membership is not null;
        IsLead = membership?.Role == ProjectMemberRole.Lead;
        Role = membership?.Role;
        IsLoaded = true;
    }

    public void SetOwner(bool isOwner) => IsOwner = isOwner;
}