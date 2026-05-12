using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.Settings;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Domain.Entities.TherapistEntity
{
    public class ProgressTracker
    {
        public int ID { get; set; } 
        public DateTime Date_Recorded { get; set; }
        public int Mood_Score { get; set; }
        public int Anxiety_Level { get; set; }
        public double Sleep_Hours { get; set; }

       
        public int PatientId { get; set; } 
        public virtual Patient Patient { get; set; } = default!;
    }
}
