using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class DailyMessages
    {
        public int Id { get; set; } 
        public string Message_Text { get; set; }
        public string Category { get; set; } 
        public DateTime? Date_Shown { get; set; } 

      
        public int UserId { get; set; }
        public virtual User User { get; set; } 
    }
}

