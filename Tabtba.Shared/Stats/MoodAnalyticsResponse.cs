using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Stats
{
    public class MoodAnalyticsResponse
    {
        public string PositiveVibePercentage { get; set; } = "0%";
        public List<MoodDataPoint> WeeklyChart { get; set; } = new();
    }

    public class MoodDataPoint
    {
        public string DayName { get; set; } = default!; // MON, TUE...
        public double MoodValue { get; set; }
        public int EntryCount { get; set; }
    }
}
