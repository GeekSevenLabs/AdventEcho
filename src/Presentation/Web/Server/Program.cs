using AdventEcho.Identity.Application.Shared;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Presentation.Web.Client.Services.Results;
using MudBlazor.Services;
using AdventEcho.Presentation.Web.Server.Components;
using Menso.Tools.Exceptions;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddHttpClient(ApplicationSharedConstants.HttpClients.AdventEchoIdentityName, options =>
    {
        var baseAddress = builder.Configuration["AdventEchoDomains:AdventEchoIdentity"];
        Throw.When.NullOrEmpty(baseAddress, "Base address not configured for AdventEchoIdentity");
        options.BaseAddress = new Uri(baseAddress);
    });

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddAdventEchoIdentityApplicationSharedServices();
builder.Services.AddScoped<IUiUtils, UiUtils>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AdventEcho.Presentation.Web.Client._Imports).Assembly);

app.Run();
