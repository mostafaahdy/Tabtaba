namespace Tabtaba.Shared.Therapist;

public class AddAvailabilityRequest
{
    public Guid TherapistId { get; set; }
    public List<AvailabilitySlotDto> Slots { get; set; } = new();
}

public class AvailabilitySlotDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}