using System.Diagnostics.CodeAnalysis;

namespace AdventEcho.Kernel.Application.Shared;

public readonly record struct EndpointDefinition(
    [StringSyntax(StringSyntaxAttribute.Uri)]string Endpoint, 
    string Name, 
    string Description,
    string Tag
);