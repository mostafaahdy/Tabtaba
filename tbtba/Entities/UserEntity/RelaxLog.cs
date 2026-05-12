using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class RelaxLog
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = default!;
        public int RelaxContentId { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual RelaxContent RelaxContent { get; set; } = default!;
    }
}
