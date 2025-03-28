using AdventEcho.Kernel.Infrastructure;

var builder = DistributedApplication.CreateBuilder(args);

var dbServer = builder
    .AddSqlServer(Names.DataBases.AdventEchoDataBaseVolume)
    .WithDataVolume(Names.DataBases.AdventEchoIdentityDataBase);

var identityDb = dbServer
    .AddDatabase(Names.DataBases.AdventEchoIdentityDataBase);

var identityApi = builder
    .AddProject<Projects.AdventEcho_Presentation_Identity>(Names.Projects.AdventEchoIdentity)
    .WithReference(identityDb)
    .WaitFor(identityDb);

var worker = builder
    .AddProject<Projects.AdventEcho_Orchestrator_Workers>(Names.Projects.AdventEchoOrchestratorWorkers)
    .WithReference(identityDb)
    .WaitFor(identityDb);

// builder.AddProject<Projects.AdventEcho_Orchestrator_Web>("webfrontend")
//     .WithExternalHttpEndpoints()
//     .WithReference(apiService)
//     .WaitFor(apiService);

builder.Build().Run();
