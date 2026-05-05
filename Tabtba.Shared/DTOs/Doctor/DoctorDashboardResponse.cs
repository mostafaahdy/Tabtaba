using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Doctor;

public class DoctorDashboardResponse
{
    public string DoctorName { get; set; } = string.Empty;
    public decimal TotalEarnings { get; set; }
    public double EarningsPercentage { get; set; }
    public int TotalSessions { get; set; }
    public double Rating { get; set; }
    public int RemainingSessions { get; set; }
    public List<TodaySessionDto> TodaySessionsList { get; set; } = [];
}