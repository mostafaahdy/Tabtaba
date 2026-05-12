using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.User
{
    public class UserSettingsResponse
    {
        public bool EnableNotifications { get; set; }
        public bool MoodTrackingReminders { get; set; }
        public bool DarkMode { get; set; } //  Flutter Appearance
        public string Language { get; set; } = "ar"; // default
    }
}
