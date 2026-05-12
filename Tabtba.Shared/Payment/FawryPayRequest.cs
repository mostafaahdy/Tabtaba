using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Payment;

public class FawryPayRequest
{
    public int PatientId { get; set; }
    public int AppointmentId { get; set; }
}