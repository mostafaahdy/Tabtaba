using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session
{
    public class CalendarBookingResponse
    {
        public string Title { get; set; } = "Thank You !";
        public string SubTitle { get; set; } = "Your Appointment Successful";
        public string DoctorName { get; set; } = default!;
        public string Speciality { get; set; } = default!;
        public string AppointmentDate { get; set; } = default!; // Monday, Oct 24
        public string AppointmentTime { get; set; } = default!; // 02:30 PM
        public string Duration { get; set; } = default!;      // 45 min
        public string Price { get; set; } = default!;         // $0 (Included)
        public string SessionType { get; set; } = default!;   // Virtual Sanctuary
        public string ConfirmationText { get; set; } = default!;
    }
}
