using System.Net;
using HelpDesk.Ui.Components;
using HelpDesk.Ui.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddScoped<CookieContainer>();
builder.Services
    .AddHttpClient("api", client => { client.BaseAddress = new Uri("http://localhost:5258"); })
    .ConfigurePrimaryHttpMessageHandler(sp =>
    {
        var container = sp.GetRequiredService<CookieContainer>();
        return new HttpClientHandler { CookieContainer = container, UseCookies = true };
    });

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<VoteService>();
builder.Services.AddScoped<AttachmentService>();
builder.Services.AddScoped<InviteService>();
builder.Services.AddScoped<AssignmentService>();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();