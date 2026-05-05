using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Entities;

namespace Tabtaba.Domain.Entities;

public class Withdrawal
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string Status { get; set; } = "Processing";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Doctor Doctor { get; set; } = default!;
}
