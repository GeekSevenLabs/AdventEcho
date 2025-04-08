using AdventEcho;
using AdventEcho.Identity.Application.Shared;
using AdventEcho.Presentation.Web.Client.MessageHandlers;
using AdventEcho.Presentation.Web.Client.Services.Results;
using MudBlazor.Services;
using AdventEcho.Presentation.Web.Server.Components;
using AdventEcho.Presentation.Web.Server.Extensions;
using Menso.Tools.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

builder.Services.AddCascadingAuthenticationState();

builder.Services
    .AddHttpClient(ApplicationSharedConstants.HttpClients.AdventEchoIdentityName, options =>
    {
        var baseAddress = builder.Configuration["AdventEchoDomains:AdventEchoIdentity"];
        Throw.When.NullOrEmpty(baseAddress, "Base address not configured for AdventEchoIdentity");
        options.BaseAddress = new Uri(baseAddress);
    })
    .AddHttpMessageHandler<CookieHandler>();

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddAdventEchoIdentityApplicationSharedServices();
builder.Services.AddScoped<IUiUtils, UiUtils>();
builder.Services.AddScoped<CookieHandler>();

// Add services to the container.
var options = builder.AddAdventEchoIdentityOptions();
builder.AddAdventEchoIdentitySecurityForClientServices(options);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AdventEcho.Presentation.Web.Client._Imports).Assembly)
    .AllowAnonymous();

app.Run();
