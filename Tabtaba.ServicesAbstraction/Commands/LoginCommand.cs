using MediatR;
using Tabtaba.Shared.Auth;

namespace Tabtaba.ServicesAbstraction.Commands;

public record LoginCommand(
    string Email,
    string Password,
    bool AgreeToTerms
) : IRequest<LoginResponse>;