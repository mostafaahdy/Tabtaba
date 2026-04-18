using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class Doctors
    {
        public int Id { get; set; } 
        public string Specialization { get; set; }
        public string License_Number { get; set; }
        public int Years_Experience { get; set; }
        public decimal Rating { get; set; }
        public string Availability_Status { get; set; }

       
        public int UserId { get; set; }
        public virtual User User { get; set; }

       
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
