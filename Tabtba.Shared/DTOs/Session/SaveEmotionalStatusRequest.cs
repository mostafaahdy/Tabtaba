using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Session;

public class SaveEmotionalStatusRequest
{
    public int AppointmentId { get; set; }
    public int MoodScore { get; set; }
}