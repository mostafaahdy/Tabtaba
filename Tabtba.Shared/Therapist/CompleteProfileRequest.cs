namespace Tabtaba.Shared.Therapist;

public class CompleteProfileRequest
{
    public Guid TherapistId { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
}