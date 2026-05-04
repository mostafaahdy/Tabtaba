using Tabtaba.Domain.Enums;
using Tabtaba.Entities;

namespace Tabtaba.Domain.Entities;

public class Therapist
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    
    public string? FullName { get; set; }
    public Title? Title { get; set; }
    public Gender? Gender { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? CountryOfResidence { get; set; }

 
    public string? CvUrl { get; set; }

   
    public string? Specialization { get; set; }
    public string? Bio { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? ProfilePictureUrl { get; set; }

    
    public TherapistCategory Category { get; set; }
    public TherapistStatus Status { get; set; } = TherapistStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TherapistAvailability> Availabilities { get; set; } = new List<TherapistAvailability>();
    public ICollection<TherapistDocument> Documents { get; set; } = new List<TherapistDocument>();
    public ICollection<TherapistLanguage> Languages { get; set; } = new List<TherapistLanguage>();
    
    public ICollection<TherapistEducation> Educations { get; set; } = new List<TherapistEducation>();
}