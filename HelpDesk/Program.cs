using System.Net;
using HelpDesk.ClientServices;
using HelpDesk.Components;
using HelpDesk.Data;
using HelpDesk.Exceptions;
using HelpDesk.Extensions;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// API services
builder.Services.AddControllers();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddOptionsServices();
builder.Services.AddOpenApiServices();
builder.Services.AddIdentityServices();
builder.Services.AddAuthorizationServices();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// UI services
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddScoped<CookieContainer>();
builder.Services.AddHttpClient("api",
        client => { client.BaseAddress = new Uri("http://localhost:5258"); })
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

using (var scope = app.Services.CreateScope())
{
    await SeedData.InitializeAsync(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseOpenApiUi();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/tickets")).AllowAnonymous();
app.MapControllers();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode().AllowAnonymous();

app.Run();