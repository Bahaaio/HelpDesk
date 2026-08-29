using Workbench.Modules.Projects.Enums;

namespace Workbench.ClientServices;

public interface IProjectMembershipState
{
    bool IsLoaded { get; }
    bool IsMember { get; }
    bool IsLead { get; }
    bool IsOwner { get; }
    ProjectMemberRole? Role { get; }
    Task Load(int projectId);
    void SetOwner(bool isOwner);
}
