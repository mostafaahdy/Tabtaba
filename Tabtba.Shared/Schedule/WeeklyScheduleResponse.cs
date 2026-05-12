using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Schedule;

public class WeeklyScheduleResponse
{
    public List<SessionItemDto> Sessions { get; set; } = [];
    public int TotalSessions { get; set; }
    public int VideoSessions { get; set; }
    public int ChatSessions { get; set; }
}
