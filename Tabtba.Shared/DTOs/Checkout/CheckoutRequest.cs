using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Checkout;

public class CheckoutRequest
{
    public int PatientId { get; set; }
    public int PlanId { get; set; }
    public string PaymentMethod { get; set; } = default!;
}