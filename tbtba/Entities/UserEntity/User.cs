using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Entities.TherapistEntity; // عشان يقرا الـ Therapist الصح

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = default!;
        public string UserType { get; set; } = default!;

        // Refresh Token
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

        // العلاقات المظبوطة بالمصطلحات بتاعتك
        public Patient? Patient { get; set; }
        public Therapist? Therapist { get; set; } // تعديل من Doctor لـ Therapist

        public ICollection<KidsZone> KidsZones { get; set; } = [];
        public ICollection<DailyMessages> DailyMessages { get; set; } = [];
    }
}