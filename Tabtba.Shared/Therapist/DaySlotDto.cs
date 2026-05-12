namespace Tabtaba.Shared.Therapist
{
    public class DaySlotDto
    {
        public string DayName { get; set; } = default!; 
        public int DayNumber { get; set; } 
        public DateTime FullDate { get; set; } 
        public List<TimeSlotDto> TimeSlots { get; set; } = new();
    }
}