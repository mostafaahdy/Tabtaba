using MediatR;
using Tabtaba.Shared.DTOs.Therapist;

namespace Tabtaba.ServicesAbstraction.Commands;

public record SaveAvailabilityCommand(
    Guid TherapistId,
    bool WorksAtClinic,
    List<DayAvailabilityDto> DaysAvailability
) : IRequest<bool>;