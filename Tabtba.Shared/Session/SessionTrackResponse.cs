using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session
{
    public class SessionTrackResponse
    {
        public int TotalSessions { get; set; } = 4;
        public int CompletedSessionsCount { get; set; }
        public int RemainingSessionsCount { get; set; }

        public CurrentSessionDTO? CurrentSession { get; set; }

        public List<SessionStepDTO> SessionSteps { get; set; } = new();

        public double ProgressPercentage
            => TotalSessions > 0 ? (double) CompletedSessionsCount / TotalSessions * 100 : 0;
    }
    public class SessionStepDTO
    {
        public int SessionNumber { get; set; }
        public string Status { get; set; }= default!; // "Completed", "Upcoming", "Locked"
        public DateTime? Date { get; set; }
    }
}