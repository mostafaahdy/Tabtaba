using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.Services.Features.EducationServices;

public class SaveEducationHandler : IRequestHandler<SaveEducationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveEducationHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(
        SaveEducationCommand request,
        CancellationToken cancellationToken)
    {
        var therapists = await _unitOfWork
            .GetRepository<Therapist>()
            .GetAllAsync();

        var therapist = therapists.FirstOrDefault(t => t.Id == request.TherapistId);

        if (therapist is null)
            throw new KeyNotFoundException($"Therapist {request.TherapistId} not found.");

        var education = new TherapistEducation
        {
            TherapistId = request.TherapistId,
            HighestDegree = request.HighestDegree,
            GraduationYear = request.GraduationYear,
            UniversityName = request.UniversityName
        };

        await _unitOfWork
            .GetRepository<TherapistEducation>()
            .AddAsync(education);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}