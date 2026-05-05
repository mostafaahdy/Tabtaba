using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Earnings;

public class WithdrawRequest
{
    public int DoctorId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = default!;
}