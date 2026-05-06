using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Auth;

namespace Tabtaba.ServicesAbstraction.Commands;

public record RegisterTherapistCommand(
    string FullName,
    string L_Name,
    string Email,
    string Phone,
    string Password,
    string Gender,
    string Specialization,
    int YearsOfExperience,
    string LicenseNumber
) : IRequest<RegisterResponse>;