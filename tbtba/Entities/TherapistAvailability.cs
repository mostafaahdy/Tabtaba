namespace Tabtaba.Domain.Entities;

public class TherapistAvailability
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TherapistId { get; set; }
    public Therapist Therapist { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }     
    public TimeOnly StartTime { get; set; }       
    public TimeOnly EndTime { get; set; }          
    public bool IsAvailable { get; set; } = true;
}