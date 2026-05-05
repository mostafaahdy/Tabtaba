using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tabtaba.ServicesAbstraction.Commands;

public record SaveProfessionalInfoCommand(
    Guid TherapistId,
    string ProfessionalCategory,
    string Specialization,
    int YearsOfExperience,
    string? LicensingAuthority,
    string? LicenseNumber
) : IRequest<bool>;