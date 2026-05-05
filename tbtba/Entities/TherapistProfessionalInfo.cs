using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities;

public class TherapistProfessionalInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TherapistId { get; set; }
    public Therapist Therapist { get; set; } = null!;

    public string ProfessionalCategory { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public string? LicensingAuthority { get; set; }
    public string? LicenseNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}