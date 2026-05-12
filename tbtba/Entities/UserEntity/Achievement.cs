using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class Achievement
    {
        public int Id { get; set; } 
        public string Description { get; set; } = default!;
        public int Progress_Level { get; set; }
        public DateTime Date_Created { get; set; }

       
        public int PatientId { get; set; }
        public  Patient Patient { get; set; } = default!;

    }
}
