using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Tabtaba.ServicesAbstraction.Commands;

public record SaveEducationCommand(
    Guid TherapistId,
    string HighestDegree,
    int GraduationYear,
    string UniversityName
) : IRequest<bool>;