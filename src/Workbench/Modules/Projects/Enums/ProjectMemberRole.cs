using System.Text.Json.Serialization;

namespace Workbench.Modules.Projects.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProjectMemberRole
{
    Lead,
    Member
}