using Microsoft.AspNetCore.Http;

namespace Tabtaba.Shared.Therapist;

public class UploadCvRequest
{
    public Guid TherapistId { get; set; }
    public IFormFile CvFile { get; set; } = null!;
}