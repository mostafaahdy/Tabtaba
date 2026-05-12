using MediatR;
using Microsoft.EntityFrameworkCore;
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
using Tabtaba.Shared.Doctor;

namespace Tabtaba.Services.Features.DoctorsServices
{
    public class GetDoctorReportHandler :IRequestHandler<GetDoctorReportQuery,DoctorReportResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDoctorReportHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorReportResponse> Handle(GetDoctorReportQuery request,CancellationToken cancellationToken)
        {
            var pIdStr = request.PatientId.ToString();
            var pIdInt = request.PatientId;


            var moodRecords = await _unitOfWork.GetRepository<ProgressTracker>().GetQueryable()
                .Where(p => p.PatientId == pIdInt)
                .OrderByDescending(p => p.Date_Recorded)
                .Take(7) 
                .ToListAsync(cancellationToken);

            var dailyMoods = await _unitOfWork.GetRepository<MoodLog>().GetQueryable()
                .Where(m => m.PatientId == pIdStr) 
                .OrderByDescending(m => m.Date)
                .Take(7)
                .ToListAsync(cancellationToken);


            var appointments = await _unitOfWork.GetRepository<Appointment>().GetQueryable()
                .Where(a => a.PatientId == pIdInt)
                .ToListAsync(cancellationToken);


            var totalSessions = appointments.Count;
            var completedSessions = appointments.Count(a => a.Status == AppointmentStatus.Completed);
            var totalMins = appointments.Where(a => a.Status == AppointmentStatus.Completed)
                                           .Sum(a => a.Duration_Minutes);

            double avgMood = moodRecords.Any() ? moodRecords.Average(m => m.Mood_Score) : 0;


            return new DoctorReportResponse
            {
                PatientName = "Patient Name",
                AverageMoodScore = Math.Round(avgMood,1),
                TotalSessions = totalSessions,
                CompletedSessions = completedSessions,
                CommitmentPercentage = totalSessions > 0 ? (completedSessions * 100.0 / totalSessions) : 0,
                TotalMeditationMinutes = totalMins,

                MoodHistory = moodRecords.Select(m => new MoodChartPoint
                {
                    Date = m.Date_Recorded,
                    Score = m.Mood_Score
                }).ToList(),

                DailyMoodHistory = dailyMoods.Select(m => new MoodChartPoint
                {
                    Date = m.Date,
                    Score = (double) m.Status 
                }).ToList(),

                //  (Health Status)
                HealthStatus = avgMood switch
                {
                    < 3 => "Critical",
                    < 6 => "Warning",
                    _ => "Stable"
                },
                Recommendation = avgMood < 3 ? "Please schedule an emergency session for the patient" : "Continue with the current treatment plan"
            };
        }
    }
}
