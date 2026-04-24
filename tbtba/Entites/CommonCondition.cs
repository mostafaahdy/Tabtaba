using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class CommonCondition
    {
        public int Id { get; set; } 
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;

 
        public virtual ICollection<CommonConditions_Diagnosis> CommonConditions_Diagnoses { get; set; } = [];
    }
}
