using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entites
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
