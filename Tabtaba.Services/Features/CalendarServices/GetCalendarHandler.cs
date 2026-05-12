using MediatR;
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
using Tabtaba.Shared.Calendar;

namespace Tabtaba.Services.Features.CalendarServices
{
    public class GetCalendarHandler :IRequestHandler<GetCalendarQuery,CalendarMonthResultDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCalendarHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<CalendarMonthResultDTO> Handle(GetCalendarQuery request,CancellationToken cancellationToken)
        {
            if( !int.TryParse(request.UserId,out int patientIdInt) )
            {
                return new CalendarMonthResultDTO();
            }

            var patientIdString = request.UserId;

            var startDate = new DateTime(request.Year,request.Month,1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            
            var appointmentsList = await _unitOfWork.GetRepository<Appointment>()
                 .GetQueryable()
                 .Where(a => a.PatientId ==patientIdInt && a.Date_Time >= startDate && a.Date_Time <= endDate)
                 .Select(a => a.Date_Time.Date) 
                 .ToListAsync(cancellationToken);
            var appointmentDates = appointmentsList.ToHashSet();


            var activitiesList = await _unitOfWork.GetRepository<RelaxLog>()
                .GetQueryable()
                .Where(l => l.PatientId == patientIdString && l.CompletedAt >= startDate && l.CompletedAt <= endDate)
                .Select(l => l.CompletedAt.Date)
                .ToListAsync(cancellationToken);
            var activityDates = activitiesList.ToHashSet();


            var highmoodList = await _unitOfWork.GetRepository<MoodLog>()
                .GetQueryable()
                .Where(m => m.PatientId ==patientIdString
                               && (m.Status == MoodStatus.Happy || m.Status == MoodStatus.Neutral)
                               && m.Date >= startDate
                               && m.Date <= endDate)
                .Select(m => m.Date.Date) 
                .ToListAsync(cancellationToken);
            var highMoodDates = highmoodList.ToHashSet();

            var result = new CalendarMonthResultDTO();

            for( var date = startDate; date <= endDate; date = date.AddDays(1) )
            {
                result.Days.Add(new CalendarDayDTO
                {
                    Date = date,
                    HasRecordedSession = appointmentDates.Contains(date.Date),
                    HasCompletedActivity = activityDates.Contains(date.Date),
                    HasHighMoodCheckIn = highMoodDates.Contains(date.Date)
                });
            }

            return result;
        }
    }

}
