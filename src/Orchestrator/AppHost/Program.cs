using AdventEcho.Kernel.Infrastructure;

var builder = DistributedApplication.CreateBuilder(args);

var dbServer = builder
    .AddSqlServer(InfrastructureConstants.DataBases.AdventEchoDataBaseVolume)
    .WithDataVolume(InfrastructureConstants.DataBases.AdventEchoIdentityDataBase);

var identityDb = dbServer
    .AddDatabase(InfrastructureConstants.DataBases.AdventEchoIdentityDataBase);

var identityApi = builder
    .AddProject<Projects.AdventEcho_Presentation_Identity>(InfrastructureConstants.Projects.AdventEchoIdentity)
    .WithReference(identityDb)
    .WaitFor(identityDb);

var worker = builder
    .AddProject<Projects.AdventEcho_Orchestrator_Workers>(InfrastructureConstants.Projects.AdventEchoOrchestratorWorkers)
    .WithReference(identityDb)
    .WaitFor(identityDb);

// builder.AddProject<Projects.AdventEcho_Orchestrator_Web>("webfrontend")
//     .WithExternalHttpEndpoints()
//     .WithReference(apiService)
//     .WaitFor(apiService);

builder.Build().Run();
