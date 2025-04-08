using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace AdventEcho;

public readonly record struct EndpointDefinition(
    [StringSyntax(StringSyntaxAttribute.Uri)]string Endpoint, 
    string Name, 
    string Description,
    string Tag
);