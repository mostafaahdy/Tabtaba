using System.ComponentModel.DataAnnotations;

namespace Tabtaba.Shared.DTOs.Therapist;

public class SaveAvailabilityRequest
{
    public Guid TherapistId { get; set; }

   
    public bool WorksAtClinic { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "At least one day must be provided.")]
    public List<DayAvailabilityDto> DaysAvailability { get; set; } = new();
}

public class DayAvailabilityDto
{
    public DayOfWeek DayOfWeek { get; set; }

   
    public bool IsAvailable { get; set; }

  
    public TimeOnly? FromTime { get; set; }
    public TimeOnly? ToTime { get; set; }
}