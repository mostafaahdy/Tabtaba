using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Calendar;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
    public class GetCalendarQuery :IRequest<CalendarMonthResultDTO>
    {
        public string UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public GetCalendarQuery(string userId,int month,int year)
        {
            UserId = userId;
            Month = month;
            Year = year;
        }
    }

}
