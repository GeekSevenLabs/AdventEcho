using GeekSevenLabs.AdventEcho.Infrastructure.DataAccess.Extensions;
using GeekSevenLabs.AdventEcho.Infrastructure.Identity;
using GeekSevenLabs.AdventEcho.Infrastructure.Identity.Contexts;
using GeekSevenLabs.AdventEcho.Infrastructure.Identity.Extensions;
using GeekSevenLabs.AdventEcho.IoC.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using GeekSevenLabs.AdventEcho.Presentation.Web.Components;
using GeekSevenLabs.AdventEcho.Presentation.Web.Components.Account;
using GeekSevenLabs.AdventEcho.Presentation.Web.Endpoints;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

builder.Services.AddAdventEchoServices(builder.Configuration);

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AdventEchoIdentityDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


builder.Services.AddMudServices();

var app = builder.Build();

app.MapAdventEchoEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
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
    .AddAdditionalAssemblies(typeof(GeekSevenLabs.AdventEcho.Presentation.Web.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();