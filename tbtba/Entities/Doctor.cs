using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entities
{
    public class Doctor
    {
        public int Id { get; set; } 
        public string Specialization { get; set; } = default!;
        public string License_Number { get; set; } = default!;
        public int Years_Experience { get; set; }
        public decimal Rating { get; set; }
        public string Availability_Status { get; set; } = default!;


        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;


        public virtual ICollection<Appointment> Appointments { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = [];
    }
}
