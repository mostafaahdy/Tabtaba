using MediatR;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.ServicesAbstraction.Commands;

public record SaveAvailabilityCommand(
    Guid TherapistId,
    bool WorksAtClinic,
    List<DayAvailabilityDto> DaysAvailability
) : IRequest<bool>;