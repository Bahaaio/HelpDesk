using System.Net;

namespace HelpDesk.Ui.Services;

public class CookieForwardingHandler : DelegatingHandler
{
    private readonly CookieService _cookieService;

    public CookieForwardingHandler(CookieService cookieService)
    {
        _cookieService = cookieService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var container = new CookieContainer();
        foreach (var cookie in _cookieService.Cookies)
        {
            container.Add(new Cookie(cookie.Key, cookie.Value, "/",
                request.RequestUri!.Host));
        }

        using var handler = new HttpClientHandler { CookieContainer = container };
        using var client = new HttpClient(handler) { BaseAddress = request.RequestUri };

        var newRequest = new HttpRequestMessage(request.Method, request.RequestUri);
        foreach (var header in request.Headers)
        {
            newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (request.Content is not null)
        {
            var content = await request.Content.ReadAsByteArrayAsync(cancellationToken);
            newRequest.Content = new ByteArrayContent(content);
            if (request.Content.Headers.ContentType is not null)
            {
                newRequest.Content.Headers.ContentType = request.Content.Headers.ContentType;
            }
        }

        var response = await client.SendAsync(newRequest, cancellationToken);

        var responseCookies = container.GetCookies(request.RequestUri!);
        foreach (Cookie cookie in responseCookies)
        {
            _cookieService.SetCookie(cookie.Name, cookie.Value);
        }

        return response;
    }
}
