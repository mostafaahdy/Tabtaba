using System.ComponentModel.DataAnnotations;

namespace Tabtaba.Shared.DTOs.Therapist;

public class ProfessionalInfoRequest
{
    public Guid TherapistId { get; set; }

    [Required(ErrorMessage = "Professional category is required.")]
    public string ProfessionalCategory { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specialization is required.")]
    [StringLength(200, ErrorMessage = "Specialization must not exceed 200 characters.")]
    public string Specialization { get; set; } = string.Empty;

    [Required(ErrorMessage = "Years of experience is required.")]
    [Range(0, 60, ErrorMessage = "Years of experience must be between 0 and 60.")]
    public int YearsOfExperience { get; set; }

    public string? LicensingAuthority { get; set; }
    public string? LicenseNumber { get; set; }
}