using System;
using System.Collections.Generic;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.Domain.Enums;

namespace Tabtaba.Domain.Entities.TherapistEntity
{
    public class Therapist
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // ربط الـ Therapist بالـ User (تم تحويل نوع الـ FK لـ string عشان يتوافق مع الـ IdentityUser القياسي)
        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = null!;

        // الحقول المهنية الخاصة بالمعالج فقط (تم تنظيف الحقول المتكررة اللي موجودة أوريدو في الـ User)
        public Title? Title { get; set; }
        public string? Username { get; set; }
        public string? Nationality { get; set; }
        public string? CountryOfResidence { get; set; }
        public string? CvUrl { get; set; }
        public string? Specialization { get; set; }
        public string? Bio { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string IntroAudioUrl { get; set; } = default!;
        public string AboutDescription { get; set; } = default!;

        public TherapistCategory Category { get; set; }
        public TherapistStatus Status { get; set; } = TherapistStatus.Pending;

        // تعديل نوع الوقت لـ DateTimeOffset لتوافق PostgreSQL
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // العلاقات التابعة
        public virtual TherapistProfessionalInfo? ProfessionalInfo { get; set; }
        public virtual ICollection<TherapistAvailability> Availabilities { get; set; } = new List<TherapistAvailability>();
        public virtual ICollection<TherapistDocument> Documents { get; set; } = new List<TherapistDocument>();
        public virtual ICollection<TherapistLanguage> Languages { get; set; } = new List<TherapistLanguage>();
        public virtual ICollection<TherapistEducation> Educations { get; set; } = new List<TherapistEducation>();
    }
}