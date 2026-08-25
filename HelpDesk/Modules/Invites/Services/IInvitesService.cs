using HelpDesk.Common.Exceptions;
using HelpDesk.Modules.Invites.Dtos;
using HelpDesk.Modules.Invites.Dtos.Requests;

namespace HelpDesk.Modules.Invites.Services;

/// <summary>
///     Manages technician invite codes. Handles creation, validation, and consumption.
/// </summary>
public interface IInvitesService
{
    /// <summary>
    ///     Creates a new invite code valid for the specified duration.
    /// </summary>
    /// <param name="request">The invite configuration including validity duration.</param>
    Task<InviteDto> CreateInvite(CreateInviteRequest request);

    /// <summary>
    ///     Validates an invitation code and deletes it.
    /// </summary>
    /// <param name="code">The invite code to validate.</param>
    /// <exception cref="BadRequestException">Thrown if the invite code is invalid or expired.</exception>
    Task ValidateAndConsume(string code);
}