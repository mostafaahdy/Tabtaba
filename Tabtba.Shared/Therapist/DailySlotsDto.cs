namespace Tabtaba.Shared.Therapist
{
    public class DailySlotsDto
    {
        public string DayName { get; set; } = default!; // MON
        public int DayNumber { get; set; } // 12
        public List<string> Times { get; set; } = []; // ["10:00 AM", "06:00 PM"]
    }
}