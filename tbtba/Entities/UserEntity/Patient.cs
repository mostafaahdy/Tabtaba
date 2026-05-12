using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.TherapistEntity;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class Patient
    {

        public int Id { get; set; } 
        public DateTime Date_Of_Birth { get; set; }
        public string Marital_Status { get; set; } = default!;
        public string Client_Role { get; set; } = default!;
        public string Medical_History_Summary { get; set; } = default!;
        public int RemainingSessions { get; set; } = 0;

        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;

        public bool EnableNotifications { get; set; } = true;
        public bool MoodTrackingReminders { get; set; } = true;
        public bool DarkMode { get; set; } = false;
        public string Language { get; set; } = "ar";

        public bool SessionReminders { get; set; } = true;
        public bool MoodTrackingUpdates { get; set; } = true;
        public bool TherapistMessages { get; set; } = true;
        public bool NewContentAlerts { get; set; } = true;
        public bool PersonalizedTips { get; set; } = false;
        public bool ReceiveEmails { get; set; } = true;
        public bool ReceiveNotifications { get; set; } = true;

        public ICollection<Achievement> Achievements { get; set; } = [];
        public virtual ICollection<Appointment> Appointments { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
        public virtual ICollection<Journal> Journals { get; set; } = [];
        public virtual ICollection<ProgressTracker> ProgressTrackers { get; set; } = [];
        public virtual MedicalRecord MedicalRecord { get; set; } = default!;
    }
}

