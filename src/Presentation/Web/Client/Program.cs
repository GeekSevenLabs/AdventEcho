using AdventEcho.Identity.Application.Shared;
using AdventEcho.Kernel.Application.Shared;
using AdventEcho.Presentation.Web.Client.MessageHandlers;
using AdventEcho.Presentation.Web.Client.Services.Results;
using Menso.Tools.Exceptions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddScoped<CookieHandler>();

builder.Services
    .AddHttpClient(ApplicationSharedConstants.HttpClients.AdventEchoIdentityName, options =>
    {
        var baseAddress = builder.Configuration["AdventEchoDomains:AdventEchoIdentity"];
        Throw.When.NullOrEmpty(baseAddress, "Base address not configured for AdventEchoIdentity");
        options.BaseAddress = new Uri(baseAddress);
    })
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddMudServices();
builder.Services.AddAdventEchoIdentityApplicationSharedServices();
builder.Services.AddScoped<IUiUtils, UiUtils>();

await builder.Build().RunAsync();
