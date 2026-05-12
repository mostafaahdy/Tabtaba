using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.ServicesAbstraction.Queries
{
    public record GetPatientSideTherapistProfileQuery(Guid TherapistId)
    :IRequest<PatientSideTherapistProfileResponse>;
}
