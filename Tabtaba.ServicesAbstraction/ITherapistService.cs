using Microsoft.AspNetCore.Http;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.ServicesAbstraction;

public interface ITherapistService
{
    Task<string> UploadCvAsync(Guid therapistId, IFormFile cvFile);
    Task CompleteProfileAsync(CompleteProfileRequest request);
    Task AddAvailabilityAsync(AddAvailabilityRequest request);
    Task SubmitCriteriaAsync(SubmitCriteriaRequest request);
    Task UpdatePersonalInfoAsync(UpdatePersonalInfoRequest request);
}