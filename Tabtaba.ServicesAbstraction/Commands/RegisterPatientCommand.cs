using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.Auth;

namespace Tabtaba.ServicesAbstraction.Commands;

public record RegisterPatientCommand(
    string FullName,
    string L_Name,
    string Email,
    string Phone,
    string Password,
    string Gender,
    string DateOfBirth,
    string MaritalStatus
) : IRequest<RegisterResponse>;