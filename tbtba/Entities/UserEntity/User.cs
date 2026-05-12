using Microsoft.AspNetCore.Identity;
using System.Numerics;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Entities.TherapistEntity;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string L_Name { get; set; } = default!;
        public string? ImageUrl { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = default!;
        public string User_Type { get; set; } = default!;
        public string Hashed_Password { get; set; } = default!;
        public int DoctorId { get; set; }

        //  Refresh Token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public Patient Patient { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
        public ICollection<KidsZone> KidsZones { get; set; } = [];
        public ICollection<DailyMessages> DailyMessages { get; set; } = [];
          }
}