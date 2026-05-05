using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Auth;

namespace Tabtaba.ServicesAbstraction.Commands;

public record ResetPasswordCommand(
    string Email,
    string ResetToken,
    string NewPassword,
    string ConfirmPassword
) : IRequest<ResetPasswordResponse>;