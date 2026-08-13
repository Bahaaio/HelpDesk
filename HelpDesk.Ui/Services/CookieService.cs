using System.Collections.Concurrent;

namespace HelpDesk.Ui.Services;

public class CookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ConcurrentDictionary<string, string> _cookies = new();

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        CaptureCookies();
    }

    private void CaptureCookies()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.Request.Cookies == null) return;

        foreach (var cookie in httpContext.Request.Cookies)
        {
            _cookies[cookie.Key] = cookie.Value;
        }
    }

    public IReadOnlyDictionary<string, string> Cookies => _cookies;

    public void SetCookie(string name, string value)
    {
        _cookies[name] = value;
    }
}
