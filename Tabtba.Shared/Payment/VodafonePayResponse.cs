using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Payment;

public class VodafonePayResponse
{
    public int PaymentId { get; set; }
    public string Status { get; set; } = default!;
    public decimal AmountDue { get; set; }
    public decimal ServiceFee { get; set; }
    public decimal TotalAmount { get; set; }
    public string WalletPhoneNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}