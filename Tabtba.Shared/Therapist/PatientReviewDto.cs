namespace Tabtaba.Shared.Therapist
{
    public class PatientReviewDto
    {
        public double StarRating { get; set; }
        public string? Comment { get; set; } = default!;
    }
}