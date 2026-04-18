using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class Patient
    {

        public int Id { get; set; } 
        public DateTime Date_Of_Birth { get; set; }
        public string Marital_Status { get; set; }
        public string Client_Role { get; set; } 
        public string Medical_History_Summary { get; set; }

       
        public int UserId { get; set; }
        public virtual User User { get; set; }


        public ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Journal> Journals { get; set; }
        public virtual ICollection<ProgressTracker> ProgressTrackers { get; set; }
        public virtual MedicalRecord MedicalRecord { get; set; } 
    }
}

