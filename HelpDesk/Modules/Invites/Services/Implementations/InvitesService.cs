using HelpDesk.Common.Exceptions;
using HelpDesk.Data.Persistence;
using HelpDesk.Modules.Invites.Dtos;
using HelpDesk.Modules.Invites.Dtos.Requests;
using HelpDesk.Modules.Invites.Models;
using HelpDesk.Modules.Invites.Repositories;

namespace HelpDesk.Modules.Invites.Services.Implementations;

public class InvitesService : IInvitesService
{
    private readonly IInvitesRepository _invitesRepository;
    private readonly ITokensService _tokensService;
    private readonly IUnitOfWork _unitOfWork;

    public InvitesService(IInvitesRepository invitesRepository, ITokensService tokensService,
        IUnitOfWork unitOfWork)
    {
        _invitesRepository = invitesRepository;
        _tokensService = tokensService;
        _unitOfWork = unitOfWork;
    }

    public async Task<InviteDto> CreateInvite(CreateInviteRequest request)
    {
        var invite = new InviteCode
        {
            Code = _tokensService.Generate(8),
            ExpiresAt = DateTime.UtcNow.AddDays(request.ValidDays)
        };

        _invitesRepository.Add(invite);
        await _unitOfWork.SaveChangesAsync();

        return new InviteDto(invite.Code, invite.ExpiresAt);
    }

    public async Task ValidateAndConsume(string code)
    {
        var invite = await _invitesRepository.FindAsync(code);

        if (invite is null || invite.ExpiresAt < DateTime.UtcNow)
            throw new BadRequestException("Invalid or expired invite code");

        _invitesRepository.Remove(invite);
        await _unitOfWork.SaveChangesAsync();
    }
}