using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Payment;

public class TransactionResponse
{
    public int PaymentId { get; set; }
    public string TransactionId { get; set; } = default!;
    public decimal AmountPaid { get; set; }
    public string Status { get; set; } = default!;
    public DateTime Date { get; set; }
    public string Time { get; set; } = default!;
    public AppointmentSummaryDto AppointmentSummary { get; set; } = default!;
}

public class AppointmentSummaryDto
{
    public string DoctorName { get; set; } = default!;
    public string? DoctorProfilePicture { get; set; }
    public string Specialization { get; set; } = default!;
    public DateTime AppointmentDate { get; set; }
}