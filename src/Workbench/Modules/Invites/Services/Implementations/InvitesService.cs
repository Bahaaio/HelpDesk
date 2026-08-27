using Workbench.Common.Exceptions;
using Workbench.Data.Persistence;
using Workbench.Modules.Invites.Dtos;
using Workbench.Modules.Invites.Dtos.Requests;
using Workbench.Modules.Invites.Models;
using Workbench.Modules.Invites.Repositories;

namespace Workbench.Modules.Invites.Services.Implementations;

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
