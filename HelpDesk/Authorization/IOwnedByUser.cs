namespace HelpDesk.Authorization;

public interface IOwnedByUser
{
    int OwnerId { get; }
}
