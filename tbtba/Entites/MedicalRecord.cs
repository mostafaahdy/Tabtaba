using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class MedicalRecord
    {
        public int Id { get; set; } 
        public DateTime Creation_Date { get; set; }
        public DateTime Last_Updated_Date { get; set; }

       
        public int PatientId { get; set; } 
        public virtual Patient Patient { get; set; } 

        
        public virtual ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
