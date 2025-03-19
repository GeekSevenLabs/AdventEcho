using AdventEcho.Identity.Infrastructure.Models;
using AdventEcho.Identity.IoC;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Presentation.Identity.Documentations;
using AdventEcho.Presentation.Identity.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Kernel Server
builder.Services.AddAdventEchoServerDocumentation(options =>
{
    options.AddDocumentTransformer<IdentityOpenApiDocumentTransformer>();
});

// Identity IoC
builder.Services.AddAdventEchoIdentity(builder.Configuration);

var app = builder.Build();

app.MapAccountEndpoints();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Kernel Server
app.MapAdventEchoServerDocumentation();

var v1Group = app.MapGroup("/v1");
v1Group.MapHealthCheck();


app.Run();
