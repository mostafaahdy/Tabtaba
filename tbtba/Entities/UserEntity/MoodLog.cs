using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.Enums;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class MoodLog
    {
        public int Id { get; set; }
        public MoodStatus Status { get; set; } = default!;
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string PatientId { get; set; } = default!;
        public User Patient { get; set; } = default!;
    }
}
