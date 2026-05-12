using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Therapist;

public class TherapistProfileResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? ProfilePictureUrl { get; set; }
    public string? Specialization { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? Bio { get; set; }
    public decimal Rating { get; set; }
    public int TotalPatients { get; set; }
    public int TotalSessions { get; set; }
    public List<EducationDto> Educations { get; set; } = [];
    public ProfessionalInfoDto? ProfessionalInfo { get; set; }
    public List<AvailabilityDto> Availabilities { get; set; } = [];
    public List<SessionPricingDto> SessionPricing { get; set; } = [];
}

public class EducationDto
{
    public string HighestDegree { get; set; } = default!;
    public string UniversityName { get; set; } = default!;
    public int GraduationYear { get; set; }
}

public class ProfessionalInfoDto
{
    public string? LicenseNumber { get; set; }
    public string? LicensingAuthority { get; set; }
    public string? Specialization { get; set; }
    public int YearsOfExperience { get; set; }
}

public class AvailabilityDto
{
    public string DayOfWeek { get; set; } = default!;
    public string StartTime { get; set; } = default!;
    public string EndTime { get; set; } = default!;
    public bool IsAvailable { get; set; }
}

public class SessionPricingDto
{
    public string SessionType { get; set; } = default!;
    public decimal Price { get; set; }
}