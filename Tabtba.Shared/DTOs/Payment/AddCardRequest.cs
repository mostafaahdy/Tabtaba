using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Payment;

public class AddCardRequest
{
    public string CardHolderName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string ExpiryDate { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string UserId { get; set; } = default!;
}