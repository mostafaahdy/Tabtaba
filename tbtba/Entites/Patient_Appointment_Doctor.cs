using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;



namespace Tabtaba.Entites
{
    public class Patient_Appointment_Doctor
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }

        public Patient Patient { get; set; } = default!;
        public Appointment Appointment { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
    }
}


