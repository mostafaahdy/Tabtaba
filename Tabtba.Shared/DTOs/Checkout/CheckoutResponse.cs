using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Checkout;

public class CheckoutResponse
{
    public int SubscriptionId { get; set; }
    public string PlanName { get; set; } = default!;
    public decimal Price { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}