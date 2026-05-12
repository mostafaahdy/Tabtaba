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
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Doctor;
using Tabtaba.Shared.Stats;

namespace Tabtaba.Services.Features.PatientServices
{
    public class GetMoodAnalyticsHandler :IRequestHandler <GetMoodAnalyticsQuery,MoodAnalyticsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMoodAnalyticsHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<MoodAnalyticsResponse> Handle(GetMoodAnalyticsQuery request,CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lastWeek = DateTime.UtcNow.Date.AddDays(-6); 

            var logs = await _unitOfWork.GetRepository<MoodLog>()
                .GetQueryable()
                .Where(m => m.PatientId == userId && m.Date >= lastWeek)
                .ToListAsync(cancellationToken);


            var positiveCount = logs.Count(l => (int) l.Status >= 4);
            var total = logs.Count > 0 ? logs.Count : 1;
            var percentage = (positiveCount * 100) / total;

            // Line & Bar
            var daysOfWeek = Enumerable.Range(0,7)
                .Select(i => lastWeek.AddDays(i))
                .ToList();
            var chartData = daysOfWeek.Select(day =>
            {
                var dayLogs = logs.Where(l => l.Date.Date == day.Date).ToList();
                return new MoodDataPoint
                {
                    DayName = day.DayOfWeek.ToString().Substring(0,3).ToUpper(),
                    MoodValue = dayLogs.Any() ? Math.Round(dayLogs.Average(l => (int) l.Status),1) : 0,
                    EntryCount = dayLogs.Count
                };
            }).ToList();

            return new MoodAnalyticsResponse
            {
                PositiveVibePercentage = $"{percentage}% Positive Vibes",
                WeeklyChart = chartData
            };
        }
    }
}
   

