using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Payment;

public class VodafonePayRequest
{
    public int PatientId { get; set; }
    public string WalletPhoneNumber { get; set; } = default!;
    public int AppointmentId { get; set; }
}