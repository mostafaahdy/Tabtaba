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
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Media_Type { get; set; } = default!;


        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}
