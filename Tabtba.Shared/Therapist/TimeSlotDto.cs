namespace Tabtaba.Shared.Therapist
{
    public class TimeSlotDto
    {
        public string TimeLabel { get; set; } = default!; 
        public string MilitaryTime { get; set; } = default!; 
        public bool IsBooked { get; set; }
    }
}