using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class DailyMessages
    {
        public int Id { get; set; } 
        public string Message_Text { get; set; } = default!;
        public string Category { get; set; } = default!;
        public DateTime? Date_Shown { get; set; } 

      
        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}

