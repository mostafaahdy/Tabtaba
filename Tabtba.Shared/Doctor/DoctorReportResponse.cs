using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Doctor
{
   public class DoctorReportResponse
    {
        public string PatientName { get; set; } = string.Empty;
        public double AverageMoodScore { get; set; }

        public int TotalSessions { get; set; }
        public int CompletedSessions { get; set; }
        public double CommitmentPercentage { get; set; }

        public int TotalMeditationMinutes { get; set; }

        public List<MoodChartPoint> MoodHistory { get; set; } = new(); //Clinical Data (Doctor)
        public List<MoodChartPoint> DailyMoodHistory { get; set; } = new();//Daily Life Data (Patient)


        public string HealthStatus { get; set; } = "Stable";
        public string Recommendation { get; set; } = string.Empty;
    }

    
    public class MoodChartPoint
    {
        public DateTime Date { get; set; }
        public double Score { get; set; }
    }

}
