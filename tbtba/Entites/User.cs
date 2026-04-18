using System.Numerics;

namespace Tabtaba.Entites
{
    public class User
    {
        public int Id { get; set; } 
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string User_Type { get; set; } 
        public string Hashed_Password { get; set; }

        public int DoctorId { get; set; }
        public  Patient Patient { get; set; }
        public  Doctors Doctor { get; set; }

       
        public  ICollection<KnowledgeZone> KnowledgeZones { get; set; }
        public  ICollection<KidsZone> KidsZones { get; set; }
        public  ICollection<DailyMessages> DailyMessages { get; set; }
    }
}

