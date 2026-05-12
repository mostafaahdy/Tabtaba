using Microsoft.AspNetCore.Http;

namespace Tabtaba.Shared.Therapist;

public class UploadVerificationRequest
{
    public Guid TherapistId { get; set; }

    
    public IFormFile CvFile { get; set; } = null!;

    
    public List<IFormFile> CertificateFiles { get; set; } = new();
}