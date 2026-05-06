using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Payment;

public class AddCardResponse
{
    public int Id { get; set; }
    public string CardHolderName { get; set; } = default!;
    public string MaskedCardNumber { get; set; } = default!;
    public string ExpiryDate { get; set; } = default!;
    public bool IsDefault { get; set; }
}