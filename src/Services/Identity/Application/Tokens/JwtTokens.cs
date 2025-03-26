using AdventEcho.Identity.Application.Shared;

namespace AdventEcho.Identity.Application.Tokens;

public record JwtTokens(JwtToken Access, JwtToken Refresh);