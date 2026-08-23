using HelpDesk.Components;
using HelpDesk.Exceptions;
using HelpDesk.Extensions;

var builder = WebApplication.CreateBuilder(args);

// API services
builder.Services.AddControllers();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddOptionsServices();
builder.Services.AddOpenApiServices();
builder.Services.AddIdentityServices();
builder.Services.AddAuthorizationServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// UI services
builder.Services.AddUiServices();

var app = builder.Build();

await app.SeedDataAsync();

if (app.Environment.IsDevelopment())
    app.UseOpenApiUi();

if (!app.Environment.IsDevelopment())
{
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

app.MapControllers();
app.MapGet("/", () => Results.Redirect("/issues")).AllowAnonymous();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode().AllowAnonymous();

app.Run();