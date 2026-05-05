using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Schedule;

public class SessionItemDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateTime SessionTime { get; set; }
    public int DurationMinutes { get; set; }
    public string SessionType { get; set; } = string.Empty;
    public string LocationMode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}