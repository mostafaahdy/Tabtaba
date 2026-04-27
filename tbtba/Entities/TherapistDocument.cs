using Tabtaba.Domain.Enums;

namespace Tabtaba.Domain.Entities;

public class TherapistDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();

    
    public Guid TherapistId { get; set; }
    public Therapist Therapist { get; set; } = null!;

    public DocumentType DocumentType { get; set; }
    public string FileUrl { get; set; } = string.Empty;   
    public string? Notes { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public bool IsVerified { get; set; } = false;          
}