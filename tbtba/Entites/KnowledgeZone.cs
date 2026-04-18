using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
{
    public class KnowledgeZone
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Media_Type { get; set; }

       
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
