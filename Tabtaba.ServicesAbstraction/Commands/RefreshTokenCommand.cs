using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.Auth;

namespace Tabtaba.ServicesAbstraction.Commands;

public record RefreshTokenCommand(
    string AccessToken,
    string RefreshToken
) : IRequest<LoginResponse>;