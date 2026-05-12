using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.User;

namespace Tabtaba.Services.Features.PatientServices
{
    public class GetUserHomeHandler :IRequestHandler<GetUserHomeQuery,UserHomeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserHomeHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<UserHomeResponse> Handle(GetUserHomeQuery request,CancellationToken cancellationToken)
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdStr,out int userIdInt);

            var moodDates = await _unitOfWork.GetRepository<MoodLog>().GetQueryable()
                .Where(m => m.PatientId == userIdStr).Select(m => m.Date.Date)
                .Distinct().OrderByDescending(d => d).ToListAsync(cancellationToken);
            int streak = 0;
            var today = DateTime.UtcNow.Date;
            for( int i = 0; i < moodDates.Count; i++ )
            {
                if( moodDates[i] == today.AddDays(-i) ) streak++;
                else break;
            }

            var moodEntries = await _unitOfWork.GetRepository<MoodLog>().GetQueryable()
                .Where(m => m.PatientId == userIdStr).Select(m => (int) m.Status).ToListAsync(cancellationToken);
            double averageMood = moodEntries.Any() ? Math.Round(moodEntries.Average(),1) : 0;

            var startOfThisWeek = DateTime.UtcNow.Date.AddDays(-(int) DateTime.UtcNow.DayOfWeek);
            var appRepo = _unitOfWork.GetRepository<Appointment>().GetQueryable()
                .Where(a => a.PatientId == userIdInt && a.Status == AppointmentStatus.Completed);

            var totalMins = await appRepo.SumAsync(a => a.Duration_Minutes,cancellationToken);
            var thisWeekMins = await appRepo.Where(a => a.Date_Time >= startOfThisWeek).SumAsync(a =>  a.Duration_Minutes,cancellationToken);
            string increaseRate = thisWeekMins > 0 ? $"+{Math.Min(100,(thisWeekMins * 100) / (totalMins > 0 ? totalMins : 1))}% this week" : "+0% this week";

            var achievements = await _unitOfWork.GetRepository<Achievement>().GetQueryable()
                .Where(a => a.PatientId == userIdInt).ToListAsync(cancellationToken);

            var topAct = achievements.GroupBy(a => a.Description).
             OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault() ?? "Deep Breathing";

            var lastMoodRecord = await _unitOfWork.GetRepository<MoodLog>().GetQueryable()
                .Where(m => m.PatientId == userIdStr).OrderByDescending(m => m.Date).FirstOrDefaultAsync(cancellationToken);

            return new UserHomeResponse
            {
                CurrentStatus = lastMoodRecord?.Status.ToString() ?? "Neutral",
                StreakDays = streak,
                MeditationMins = totalMins,
                MeditationIncreaseRate = increaseRate,
                ActivitiesDone = achievements.Count,
                TopActivity = $"Top: {topAct}",
                MoodAverage = averageMood,
                TotalMoodEntries = moodEntries.Count // Based on X entries
            };
        }
    }
        
}
