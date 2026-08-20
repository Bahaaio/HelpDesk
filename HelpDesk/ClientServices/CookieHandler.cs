using System.Net;

namespace HelpDesk.ClientServices;

public class CookieHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var cookie = _httpContextAccessor.HttpContext?.Request.Cookies["HelpDesk.Auth"];
        if (!string.IsNullOrEmpty(cookie))
        {
            request.Headers.TryAddWithoutValidation("Cookie", $"HelpDesk.Auth={cookie}");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
