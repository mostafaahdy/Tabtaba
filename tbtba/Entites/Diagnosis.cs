using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class Diagnosis
    {
        public int Id { get; set; } 
        public DateTime Diagnosis_Date { get; set; }
        public string Diagnosis_Details { get; set; } = default!;

       
        public int RecordID { get; set; } 
        public int ConditionId { get; set; } 
        public int DoctorId { get; set; } 

        public virtual MedicalRecord MedicalRecord { get; set; } = default!;
        public virtual Condition Condition { get; set; } = default!;
        public virtual Doctor Doctor { get; set; } = default!;
    }
}
