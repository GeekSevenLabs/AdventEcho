using AdventEcho.Presentation.Identity.Extensions;

(await WebApplication
    .CreateBuilder(args)
    .AddAdventEchoIdentityServices()
    .Build()
    .MapAdventEchoIdentity())
    .Run();
