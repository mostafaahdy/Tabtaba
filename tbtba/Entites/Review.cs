using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class Review
    {
        public int id { get; set; } 
        public decimal Rating_Score { get; set; }
        public string Review_Text { get; set; }

       
        public int PatientId { get; set; } 
        public int DoctorId{ get; set; } 

       
        public virtual Patient Patient { get; set; }
        public virtual Doctors Doctor { get; set; }
    }
}
