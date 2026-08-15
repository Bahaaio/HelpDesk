using System.Net.Http.Json;
using System.Text.Json;
using MudBlazor.Services;
using HelpDesk.Ui.Components;
using HelpDesk.Ui.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<CookieService>();
builder.Services.AddTransient<CookieForwardingHandler>();

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5258");
}).AddHttpMessageHandler<CookieForwardingHandler>();

builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<VoteService>();
    builder.Services.AddScoped<AttachmentService>();
    builder.Services.AddScoped<InviteService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapGet("/", () => Results.Redirect("/tickets"));

const string authCookieName = "HelpDesk.Auth";
var cookieOptions = new CookieOptions
{
    HttpOnly = true,
    SameSite = SameSiteMode.Lax,
    Path = "/",
    MaxAge = TimeSpan.FromDays(7)
};

app.MapPost("/auth/login", async (HttpContext http, IHttpClientFactory httpClientFactory) =>
{
    var body = await http.Request.ReadFromJsonAsync<JsonElement>();
    var client = httpClientFactory.CreateClient("api");
    var resp = await client.PostAsJsonAsync("/api/auth/login", body);

    if (!resp.IsSuccessStatusCode)
    {
        var error = await resp.Content.ReadAsStringAsync();
        return Results.Content(error, "application/json", statusCode: (int)resp.StatusCode);
    }

    if (resp.Headers.TryGetValues("Set-Cookie", out var cookies))
    {
        foreach (var cookie in cookies)
        {
            var nameValue = cookie.Split(';')[0].Split('=', 2);
            if (nameValue.Length == 2 && nameValue[0].Trim() == authCookieName)
            {
                http.Response.Cookies.Append(authCookieName, nameValue[1].Trim(), cookieOptions);
            }
        }
    }

    return Results.Ok();
});

app.MapPost("/auth/register", async (HttpContext http, IHttpClientFactory httpClientFactory) =>
{
    var body = await http.Request.ReadFromJsonAsync<JsonElement>();
    var client = httpClientFactory.CreateClient("api");
    var resp = await client.PostAsJsonAsync("/api/auth/register", body);

    if (!resp.IsSuccessStatusCode)
    {
        var error = await resp.Content.ReadAsStringAsync();
        return Results.Content(error, "application/json", statusCode: (int)resp.StatusCode);
    }

    return Results.Ok();
});

app.MapPost("/auth/logout", async (HttpContext http, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient("api");
    await client.PostAsync("/api/auth/logout", null);
    http.Response.Cookies.Delete(authCookieName);
    return Results.Ok();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
