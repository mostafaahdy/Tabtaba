using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace Tabtaba.Entites
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string F_Name { get; set; }  = default!;
        public string L_Name { get; set; }  = default!;
        public string Phone { get; set; }   = default!;
        public string Email { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string User_Type { get; set; } = default!;
        public string Hashed_Password { get; set; } = default!;

        public int DoctorId { get; set; }
        public  Patient Patient { get; set; } = default!;
        public  Doctor Doctor { get; set; } = default!;


        public ICollection<KnowledgeZone> KnowledgeZones { get; set; } = [];
        public  ICollection<KidsZone> KidsZones { get; set; } = [];
        public  ICollection<DailyMessages> DailyMessages { get; set; } = [];
    }
}

