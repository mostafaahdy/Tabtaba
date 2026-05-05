using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Doctor;

public class TodaySessionDto
{
    public string PatientName { get; set; } = string.Empty;
    public string SessionType { get; set; } = string.Empty;
    public DateTime SessionTime { get; set; }
}