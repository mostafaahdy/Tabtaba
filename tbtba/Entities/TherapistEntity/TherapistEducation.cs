using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.TherapistEntity;

public class TherapistEducation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid TherapistId { get; set; }
    public Therapist Therapist { get; set; } = null!;

    public string HighestDegree { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
    public string UniversityName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}