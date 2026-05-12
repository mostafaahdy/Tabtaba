using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session
{
    public class SessionReminderResponse
    {
        // Your session with Dr. yasser starts in 10 minutes
        public string DoctorName { get; set; } = default!;
        public string Speciality { get; set; } = default!; // Specialist in Mindfulness
        public string FullDate { get; set; } = default!;
        public string TimeLabel { get; set; } = default!; // "2:30 PM (45m)"
        public string SessionType { get; set; } = default!; // Virtual Sanctuary Session
        public string? DoctorImageUrl { get; set; }
        public string ZoomUrl { get; set; } = default!;
        public bool IsLiveNow { get; set; }
    }
}
