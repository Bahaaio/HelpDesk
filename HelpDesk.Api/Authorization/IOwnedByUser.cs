namespace HelpDesk.Api.Authorization;

public interface IOwnedByUser
{
    int OwnerId { get; }
}