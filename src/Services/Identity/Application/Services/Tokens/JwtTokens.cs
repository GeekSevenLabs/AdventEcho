using AdventEcho.Identity.Application.Shared.Accounts;

namespace AdventEcho.Identity.Application.Services.Tokens;

public record JwtTokens(JwtToken Access, JwtToken Refresh);