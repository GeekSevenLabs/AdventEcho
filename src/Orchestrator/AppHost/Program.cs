using AdventEcho.Orchestrator.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var dbServer = builder
    .AddSqlServer(Names.DataBases.AdventEchoDbServer)
    .WithDataVolume(Names.DataBases.AdventEchoIdentityDataBase);

var identityDb = dbServer
    .AddDatabase(Names.DataBases.AdventEchoIdentityDataBase);

var identityApi = builder
    .AddProject<Projects.AdventEcho_Presentation_Identity>(Names.Projects.AdventEchoIdentity)
    .WithReference(identityDb);

// builder.AddProject<Projects.AdventEcho_Orchestrator_Web>("webfrontend")
//     .WithExternalHttpEndpoints()
//     .WithReference(apiService)
//     .WaitFor(apiService);

builder.Build().Run();
