using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Identity.IoC;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;
using AdventEcho.Presentation.Identity.Documentations;
using AdventEcho.Presentation.Identity.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails();



// Kernel Server
builder.Services.AddAdventEchoOpenApi(options =>
{
    options.AddOperationTransformer<UseFluentValidatorRulesOperationTransformer>();
    options.AddDocumentTransformer<IdentityOpenApiDocumentTransformer>();
});

// Identity IoC
builder.AddAdventEchoSecurity();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    await app.ApplyMigrationsAsync();
}

app.MapAdventEchoIdentityVersionOneEndpoints();
app.UseAdventEchoSecurity();

// Kernel Server Endpoints
app.MapAdventEchoOpenApi();
var v1Group = app.MapGroup("/v1");
v1Group.MapHealthCheck();
v1Group.MapHealthCheckAuthorized();


await app.RunAsync();
