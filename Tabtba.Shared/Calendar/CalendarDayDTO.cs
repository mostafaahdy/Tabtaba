using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Calendar
{
    public class CalendarDayDTO
    {
        public DateTime Date { get; set; }
        public bool HasRecordedSession { get; set; } // Blue color
        public bool HasCompletedActivity { get; set; } // Green color
        public bool HasHighMoodCheckIn { get; set; } // Brown color
    }

    public class CalendarMonthResultDTO
    {
        public List<CalendarDayDTO> Days { get; set; } = new();
    }
}
