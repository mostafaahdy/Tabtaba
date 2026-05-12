using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session;

public class SaveSessionNoteRequest
{
    public int AppointmentId { get; set; }
    public string Notes { get; set; } = string.Empty;
}