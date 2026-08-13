using HelpDesk.Api.Dtos.Requests;

namespace HelpDesk.Api.Services;

public interface IAuthService
{
    Task Register(RegisterRequest request);
    Task Login(LoginRequest request);
    Task Logout();
}