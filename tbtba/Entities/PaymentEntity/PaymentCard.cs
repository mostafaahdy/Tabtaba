using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.BaymentgatewayEntity;

public class PaymentCard
{
    public int Id { get; set; }
    public string CardHolderName { get; set; } = default!;
    public string MaskedCardNumber { get; set; } = default!; // بنحفظ آخر 4 أرقام بس
    public string ExpiryDate { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public bool IsDefault { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
