using AdventEcho.Identity.Application.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAdventEchoIdentityForClient();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
