using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Session;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record ConfirmBookingCommand(
     Guid TherapistId,
     int PatientId,
     int DoctorIntId,
     DateTime SelectedDate,
     string SelectedTime,
     string SessionType,
     decimal Price,
    int Duration,
     string? ZoomUrl,
     string? ZoomId    
 ) :IRequest<BookingResponseDTO>;
}
