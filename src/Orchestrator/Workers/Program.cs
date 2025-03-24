using AdventEcho.Identity.Infrastructure.Contexts;
using AdventEcho.Kernel.Infrastructure;
using AdventEcho.Orchestrator.ServiceDefaults;
using AdventEcho.Orchestrator.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<AdventEchoIdentityDbContext>(Names.DataBases.AdventEchoIdentityDataBase);

var host = builder.Build();
host.Run();
