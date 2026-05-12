using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record CreateAppointmentCommand(
    DateTime Date_Time,
    string Session_Type,
    int Duration_Minutes,
    string Location_Mode,
    decimal Price,
    int PatientId,
    int DoctorId
) :IRequest<bool>;
}
