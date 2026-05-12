using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.TherapistEntity
{
    public class Condition
    {
        public int Id { get; set; } 
        public string Condition_Name { get; set; } = default!;
        public string Description { get; set; } = default!;


        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = [];
        
        public virtual ICollection<CommonConditions_Diagnosis> CommonConditions_Diagnoses { get; set; } = [];
    }
}
