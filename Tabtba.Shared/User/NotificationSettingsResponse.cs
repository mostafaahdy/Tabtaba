using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.User
{
    public class NotificationSettingsResponse
    {
        public bool SessionReminders { get; set; }
        public bool MoodTrackingUpdates { get; set; }
        public bool TherapistMessages { get; set; }
        public bool NewContentAlerts { get; set; }
        public bool PersonalizedTips { get; set; }
    }
}
