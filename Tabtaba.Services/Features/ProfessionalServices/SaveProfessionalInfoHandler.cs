using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.Services.Features.ProfessionalServices;

public class SaveProfessionalInfoHandler : IRequestHandler<SaveProfessionalInfoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveProfessionalInfoHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(
        SaveProfessionalInfoCommand request,
        CancellationToken cancellationToken)
    {
        
        var therapists = await _unitOfWork
            .GetRepository<Therapist>()
            .GetAllAsync();

        var therapist = therapists.FirstOrDefault(t => t.Id == request.TherapistId);

        if (therapist is null)
            throw new KeyNotFoundException($"Therapist {request.TherapistId} not found.");

    
        var professionalInfo = new TherapistProfessionalInfo
        {
            TherapistId = request.TherapistId,
            ProfessionalCategory = request.ProfessionalCategory,
            Specialization = request.Specialization,
            YearsOfExperience = request.YearsOfExperience,
            LicensingAuthority = request.LicensingAuthority,
            LicenseNumber = request.LicenseNumber
        };

        
        await _unitOfWork
            .GetRepository<TherapistProfessionalInfo>()
            .AddAsync(professionalInfo);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}