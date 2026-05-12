using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class Journal
    {
        public int Id { get; set; } 
        public string Entry_Text { get; set; } = default!;
        public string Voice_Note_Path { get; set; } = default!;
        public DateTime Date_Created { get; set; }

      
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = default!;
    }
}
