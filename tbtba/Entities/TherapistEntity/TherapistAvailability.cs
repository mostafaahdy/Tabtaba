namespace Tabtaba.Domain.Entities.TherapistEntity;

public class TherapistAvailability
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TherapistId { get; set; }
    public Therapist Therapist { get; set; } = null!;

    public bool WorksAtClinic { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public bool IsAvailable { get; set; } = true;

    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public TimeOnly? FromTime { get; set; }
    public TimeOnly? ToTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}