using AdventEcho.Identity.Infrastructure.Extensions;
using AdventEcho.Identity.IoC;
using AdventEcho.Kernel.Server.Endpoints;
using AdventEcho.Kernel.Server.Extensions;
using AdventEcho.Kernel.Server.Validations;
using AdventEcho.Orchestrator.ServiceDefaults;
using AdventEcho.Presentation.Identity.Documentations;
using AdventEcho.Presentation.Identity.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Kernel Server
builder.Services.AddAdventEchoOpenApi(options =>
{
    options.AddOperationTransformer<UseFluentValidatorRulesOperationTransformer>();
    options.AddDocumentTransformer<IdentityOpenApiDocumentTransformer>();
});

// Identity IoC
builder.AddAdventEchoIdentity();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapAdventEchoIdentityVersionOneEndpoints();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Kernel Server
app.MapAdventEchoOpenApi();

var v1Group = app.MapGroup("/v1");
v1Group.MapHealthCheck();


app.Run();
