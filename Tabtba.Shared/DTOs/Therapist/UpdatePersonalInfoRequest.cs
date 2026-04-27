using Tabtaba.Domain.Enums;

namespace Tabtaba.Shared.DTOs.Therapist;

public class UpdatePersonalInfoRequest
{
    public Guid TherapistId { get; set; }
    public PersonalInfoDto PersonalInfo { get; set; } = null!;
}