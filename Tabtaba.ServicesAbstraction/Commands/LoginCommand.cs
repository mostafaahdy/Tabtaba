using MediatR;
using Tabtaba.Shared.DTOs.Auth;

namespace Tabtaba.ServicesAbstraction.Commands;

public record LoginCommand(
    string Email,
    string Password,
    bool AgreeToTerms
) : IRequest<LoginResponse>;