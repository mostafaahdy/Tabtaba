using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Enums;

namespace Tabtaba.Domain.Entities;

public class TherapistLanguage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    
    public Guid TherapistId { get; set; }
    public Therapist Therapist { get; set; } = null!;
    public Language Language { get; set; }
}