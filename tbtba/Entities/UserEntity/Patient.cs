using System;
using System.Collections.Generic;
using Tabtaba.Domain.Entities.TherapistEntity;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class Patient
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; } // تعديل الاسم
        public string MaritalStatus { get; set; } = default!; // تعديل الاسم
        public string ClientRole { get; set; } = default!; // تعديل الاسم
        public string MedicalHistorySummary { get; set; } = default!; // تعديل الاسم
        public int RemainingSessions { get; set; } = 0;

        // الربط بالـ User
        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;

        // الـ Settings والـ Preferences (حافظنا عليها كلها)
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

        // العلاقات والـ Collections التابعة
        public ICollection<Achievement> Achievements { get; set; } = [];
        public virtual ICollection<Appointment> Appointments { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
        public virtual ICollection<Journal> Journals { get; set; } = [];
        public virtual ICollection<ProgressTracker> ProgressTrackers { get; set; } = [];
        public virtual MedicalRecord MedicalRecord { get; set; } = default!;
    }
}