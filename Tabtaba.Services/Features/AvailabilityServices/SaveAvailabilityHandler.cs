using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.Services.Features.AvailabilityServices;

public class SaveAvailabilityHandler : IRequestHandler<SaveAvailabilityCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveAvailabilityHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(
        SaveAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        
        var therapists = await _unitOfWork
            .GetRepository<Therapist>()
            .GetAllAsync();

        var therapist = therapists.FirstOrDefault(t => t.Id == request.TherapistId);

        if (therapist is null)
            throw new KeyNotFoundException($"Therapist {request.TherapistId} not found.");

        
        var existingAvailabilities = await _unitOfWork
            .GetRepository<TherapistAvailability>()
            .GetAllAsync();

        var therapistAvailabilities = existingAvailabilities
            .Where(a => a.TherapistId == request.TherapistId)
            .ToList();

        foreach (var availability in therapistAvailabilities)
            _unitOfWork.GetRepository<TherapistAvailability>().Remove(availability);

        
        var newAvailabilities = request.DaysAvailability.Select(day => new TherapistAvailability
        {
            TherapistId = request.TherapistId,
            WorksAtClinic = request.WorksAtClinic,
            DayOfWeek = day.DayOfWeek,
            IsAvailable = day.IsAvailable,
            FromTime = day.IsAvailable ? day.FromTime : null,
            ToTime = day.IsAvailable ? day.ToTime : null
        }).ToList();

        foreach (var availability in newAvailabilities)
            await _unitOfWork
                .GetRepository<TherapistAvailability>()
                .AddAsync(availability);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}