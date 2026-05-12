using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Session;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record CreateCalendarBookingCommand(
     Guid TherapistId,
     int DoctorId,
     int PatientId,
     DateTime SelectedDate,
     string SelectedTime,   
     string SessionType,
     int Duration_Minutes
 ) :IRequest<CalendarBookingResponse>;
}
