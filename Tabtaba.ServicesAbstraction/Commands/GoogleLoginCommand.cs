using MediatR;
using Tabtaba.Shared.Auth;

namespace Tabtaba.ServicesAbstraction.Commands;

public record GoogleLoginCommand(
    string Email,
    string Name
) : IRequest<LoginResponse>;