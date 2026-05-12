using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.User
{
    public class PrivacySettingsResponse
    {
        public bool ReceiveEmails { get; set; }
        public bool ReceiveNotifications { get; set; }
        public string LastUpdated { get; set; } = "May 2026";
    }
}
