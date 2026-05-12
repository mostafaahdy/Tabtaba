using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Entities.TherapistEntity;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date_Time { get; set; }
        public string Session_Type { get; set; } = default!;
        public int Duration_Minutes { get; set; }
        public string Location_Mode { get; set; } = default!;
        public AppointmentStatus Status { get; set; } = default!;
        public decimal Price { get; set; }
        public string? Zoom_Meeting_Url { get; set; }
        public string? Zoom_Meeting_Id { get; set; }
        public bool IsPaid { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
    }
}