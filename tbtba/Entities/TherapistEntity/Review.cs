using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Domain.Entities.TherapistEntity
{
    public class Review
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; } = default!;

        public int Rating_Score { get; set; }

        public string Review_Text { get; set; } = default!; 
        public string? Patient_Notes { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public virtual Patient Patient { get; set; } = default!;
        public virtual Doctor Doctor { get; set; } = default!;
        public int TherapistId { get; set; }
        public virtual Therapist Therapist { get; set; } = default!;
    }
}
