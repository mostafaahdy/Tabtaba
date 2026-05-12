using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.RelaxZone
{
    public class RelaxContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Category { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public string? MediaUrl { get; set; }
    }
}
