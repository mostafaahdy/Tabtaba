using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.Session;

namespace Tabtaba.Services.Features.SessionServices
{
    public class GetSessionReminderHandler :IRequestHandler<GetSessionReminderQuery,SessionReminderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSessionReminderHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<SessionReminderResponse> Handle(GetSessionReminderQuery request,CancellationToken cancellationToken)
        {
            var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
            var therapistRepo = _unitOfWork.GetRepository<Therapist>();

            var now = DateTime.Now;

            
            var upcomingSession = (await appointmentRepo.GetAllAsync())
                .Where(a => a.PatientId == request.PatientId
                         && a.Status == AppointmentStatus.Confirmed
                         && a.Date_Time >= now.AddMinutes(-10)) 
                .OrderBy(a => a.Date_Time)
                .FirstOrDefault();

            if( upcomingSession == null ) return null; 

            var therapist = await therapistRepo.GetByIdAsync(upcomingSession.DoctorId);

            var timeDiff = upcomingSession.Date_Time - now;
            int minutesLeft = (int) timeDiff.TotalMinutes;

            string dayPrefix = upcomingSession.Date_Time.Date == now.Date ? "Today, " : "";
            string formattedDate = dayPrefix + upcomingSession.Date_Time.ToString("MMM dd, yyyy");

            var endTime = upcomingSession.Date_Time.AddMinutes((double)upcomingSession.Duration_Minutes);
            string timeRange = $"{upcomingSession.Date_Time:HH:mm} - {endTime:HH:mm}";
            bool canJoin = (upcomingSession.Date_Time - now).TotalMinutes <= 5;
            return new SessionReminderResponse
            {
                DoctorName = therapist?.FullName ?? "No name",
                Speciality = therapist?.Specialization ?? "Specialist in Mindfulness-Based Stress Reduction",
                FullDate = upcomingSession.Date_Time.ToString("MMM dd"), // Oct 24
                TimeLabel = $"{upcomingSession.Date_Time:h:mm tt} ({upcomingSession.Duration_Minutes}m)", // 2:30 PM (45m)
                SessionType = upcomingSession.Session_Type == "Video" ? "Virtual Sanctuary Session" : "Voice/Chat Session",
                DoctorImageUrl = therapist?.ProfilePictureUrl,
                ZoomUrl = upcomingSession.Zoom_Meeting_Url ?? "https://zoom.us/join",
                IsLiveNow = (upcomingSession.Date_Time - DateTime.Now).TotalMinutes <= 5
            };
        }
    }
     
}