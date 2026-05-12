using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.ServicesAbstraction.Queries;

public record GetTherapistProfileQuery(Guid TherapistId) : IRequest<TherapistProfileResponse>;
