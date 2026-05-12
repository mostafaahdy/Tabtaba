using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class RelaxContent
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Category { get; set; } = default!; // "Breathing", "Meditation", etc.
        public int DurationInMinutes { get; set; }
        public string? MediaUrl { get; set; }

        // One-to-Many: One content can have many logs
        public virtual ICollection<RelaxLog> Logs { get; set; } = new HashSet<RelaxLog>();
    }
}
