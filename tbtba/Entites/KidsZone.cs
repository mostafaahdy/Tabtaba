using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class KidsZone
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } 
        public string Media_Path { get; set; }

    
        public int UserId { get; set; }
        public virtual User User { get; set; } 
    }
}
