using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session
{
    public class BookingResponseDTO
    {
        public string Message { get; set; } = "Session Booked!";
        public string DoctorName { get; set; } = default!;
        public string FullDateTime { get; set; } = default!;
        public string SessionType { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Success";
    }
}
