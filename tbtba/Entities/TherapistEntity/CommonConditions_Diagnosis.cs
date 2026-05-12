using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.TherapistEntity
{
    public class CommonConditions_Diagnosis
    {
        public int Id { get; set; } 
        public int CommonCondition_ID { get; set; } 

        public virtual Diagnosis Diagnosis { get; set; } = default!;
        public virtual CommonCondition CommonCondition { get; set; } = default!;


    }
}
