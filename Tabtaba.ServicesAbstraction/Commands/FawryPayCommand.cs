using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.Payment;

namespace Tabtaba.ServicesAbstraction.Commands;

public record FawryPayCommand(
    int PatientId,
    int AppointmentId
) : IRequest<FawryPayResponse>;