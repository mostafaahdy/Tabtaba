using Tabtaba.Domain.Enums;

namespace Tabtaba.Shared.DTOs.Therapist;

public class SubmitCriteriaRequest
{
    public Guid TherapistId { get; set; }
    public TherapistCategory Category { get; set; }
    public List<DocumentSubmitDto> Documents { get; set; } = new();
}

public class DocumentSubmitDto
{
    public DocumentType DocumentType { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string? Notes { get; set; }
}