using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.User
{
    public class UserHomeResponse
    {
        public string CurrentStatus { get; set; } = "Neutral";
        public int StreakDays { get; set; }
        public int MeditationMins { get; set; }
        public string MeditationIncreaseRate { get; set; } = "+0% this week"; 
        public int ActivitiesDone { get; set; }
        public string TopActivity { get; set; } = "General";
        public double MoodAverage { get; set; }
        public int TotalMoodEntries { get; set; }
    }
}
