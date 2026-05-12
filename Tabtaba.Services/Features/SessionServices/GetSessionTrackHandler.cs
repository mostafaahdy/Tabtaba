using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.Session;
using Tabtba.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Tabtaba.Services.Features.SessionServices
{
    public class GetSessionTrackHandler :IRequestHandler<GetSessionTrackQuery,SessionTrackResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetSessionTrackHandler(ApplicationDbContext context) => _context = context;

        public async Task<SessionTrackResponse> Handle(GetSessionTrackQuery request,CancellationToken cancellationToken)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == request.PatientId,cancellationToken);

            if( patient == null )
                return new SessionTrackResponse { SessionSteps = new List<SessionStepDTO>() };

            var completedCount = await _context.Appointments
                .CountAsync(a => a.PatientId == request.PatientId && a.Status.ToString() == "Completed",cancellationToken);

            var response = new SessionTrackResponse
            {
                CompletedSessionsCount = completedCount,
                RemainingSessionsCount = patient.RemainingSessions,
                SessionSteps = new List<SessionStepDTO>()
            };

            var nextAppointment = await _context.Appointments
                .Include(a => a.Doctor) 
                .Where(a => a.PatientId == request.PatientId && a.Status.ToString() == "Upcoming")
                .OrderBy(a => a.Date_Time)
                .FirstOrDefaultAsync(cancellationToken);

            if( nextAppointment != null )
            {
                response.CurrentSession = new CurrentSessionDTO
                {
                    DoctorName = nextAppointment.Doctor.User.FullName,
                    DoctorImageUrl = nextAppointment.Doctor.User.ImageUrl,
                    Specialization = nextAppointment.Doctor.Specialization,
                    ScheduledAt = nextAppointment.Date_Time,
                    Status = nextAppointment.Status.ToString(),
                    MeetingLink = nextAppointment.Zoom_Meeting_Url,
                   
                    IsLiveNow = DateTime.Now >= nextAppointment.Date_Time.AddMinutes(-5)
                                && DateTime.Now <= nextAppointment.Date_Time.AddMinutes(45)
                };
            }

            for( int i = 1; i <= 4; i++ )
            {
                var step = new SessionStepDTO { SessionNumber = i };

                if( i <= completedCount )
                    step.Status = "Completed";
                else if( i == completedCount + 1 && patient.RemainingSessions > 0 )
                    step.Status = "Upcoming";
                else
                    step.Status = "Locked";

                response.SessionSteps.Add(step);
            }

            return response;
        }
    }

}
