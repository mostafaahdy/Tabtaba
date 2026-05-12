using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record CancelAppointmentCommand(int AppointmentId) :IRequest<bool>;
}
