using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session
{
    public class CurrentSessionDTO
    {
        public string DoctorName { get; set; } = default!;
        public string? DoctorImageUrl { get; set; }
        public string? Specialization { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Status { get; set; } = default!; // Upcoming, Ongoing
        public string? MeetingLink { get; set; }
        public bool IsLiveNow { get; set; }
    }
}
