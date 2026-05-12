using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Domain.Entities.BaymentgatewayEntity;

public class Payment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int AppointmentId { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string WalletPhoneNumber { get; set; } = default!;
    public decimal Amount { get; set; }
    public decimal ServiceFee { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Patient Patient { get; set; } = default!;
    public Appointment Appointment { get; set; } = default!;
}