using HelpDesk.Data;
using HelpDesk.Dtos.Requests;
using HelpDesk.Dtos.Responses;
using HelpDesk.Exceptions;
using HelpDesk.Models;
using HelpDesk.Services.Auth;

namespace HelpDesk.Services.Invites;

public class InvitesService : IInvitesService
{
    private readonly AppDbContext _db;
    private readonly ITokensService _tokensService;

    public InvitesService(AppDbContext db, ITokensService tokensService)
    {
        _db = db;
        _tokensService = tokensService;
    }

    public async Task<InviteDto> CreateInvite(CreateInviteRequest request)
    {
        var invite = new InviteCode
        {
            Code = _tokensService.Generate(8),
            ExpiresAt = DateTime.UtcNow.AddDays(request.ValidDays)
        };

        _db.InviteCodes.Add(invite);
        await _db.SaveChangesAsync();

        return new InviteDto(invite.Code, invite.ExpiresAt);
    }

    public async Task ValidateAndConsume(string code)
    {
        var invite = await _db.InviteCodes.FindAsync(code);

        if (invite is null || invite.ExpiresAt < DateTime.UtcNow)
            throw new BadRequestException("Invalid or expired invite code");

        _db.Remove(invite);
        await _db.SaveChangesAsync();
    }
}
