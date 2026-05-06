using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Entities;

namespace Tabtaba.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int PlanId { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string Status { get; set; } = "Pending";
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Patient Patient { get; set; } = default!;
    public Plan Plan { get; set; } = default!;
}