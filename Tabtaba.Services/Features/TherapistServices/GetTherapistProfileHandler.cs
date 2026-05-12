using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.Services.Features.TherapistServices;

public class GetTherapistProfileHandler
    : IRequestHandler<GetTherapistProfileQuery, TherapistProfileResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTherapistProfileHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<TherapistProfileResponse> Handle(
        GetTherapistProfileQuery request,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Therapist>();
        var therapists = await repo.GetAllAsync();
        var therapist = therapists.FirstOrDefault(t => t.Id == request.TherapistId);

        if (therapist is null)
            throw new Exception("Therapist not found");

        var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
        var allAppointments = await appointmentRepo.GetAllAsync();
        var therapistAppointments = allAppointments
            .Where(a => a.DoctorId != 0) //  TherapistId
            .ToList();

        return new TherapistProfileResponse
        {
            Id = therapist.Id,
            FullName = therapist.FullName ?? string.Empty,
            ProfilePictureUrl = therapist.ProfilePictureUrl,
            Specialization = therapist.Specialization,
            YearsOfExperience = therapist.YearsOfExperience,
            Bio = therapist.Bio,
            Educations = therapist.Educations.Select(e => new EducationDto
            {
                HighestDegree = e.HighestDegree,
                UniversityName = e.UniversityName,
                GraduationYear = e.GraduationYear
            }).ToList(),
            ProfessionalInfo = therapist.ProfessionalInfo is null ? null : new ProfessionalInfoDto
            {
                LicenseNumber = therapist.ProfessionalInfo.LicenseNumber,
                LicensingAuthority = therapist.ProfessionalInfo.LicensingAuthority,
                Specialization = therapist.ProfessionalInfo.Specialization,
                YearsOfExperience = therapist.ProfessionalInfo.YearsOfExperience
            },
            Availabilities = therapist.Availabilities.Select(a => new AvailabilityDto
            {
                DayOfWeek = a.DayOfWeek.ToString(),
                StartTime = a.StartTime.ToString(),
                EndTime = a.EndTime.ToString(),
                IsAvailable = a.IsAvailable
            }).ToList()
        };
    }
}