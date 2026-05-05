using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date_Time { get; set; }
        public string Session_Type { get; set; } = default!;
        public decimal Duration_Minutes { get; set; }
        public string Location_Mode { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
    }
}