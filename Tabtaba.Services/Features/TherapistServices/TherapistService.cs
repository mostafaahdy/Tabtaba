using Microsoft.AspNetCore.Http;
using Tabtaba.Shared.DTOs.Therapist;
using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Enums;
using Tabtaba.ServicesAbstraction;


namespace Tabtaba.Services.Features.TherapistServices;

public class TherapistService : ITherapistService
{
    public async Task<string> UploadCvAsync(Guid therapistId, IFormFile cvFile)
    {
        if (cvFile is null || cvFile.Length == 0)
            throw new ArgumentException("CV file is required.");

        var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
        var extension = Path.GetExtension(cvFile.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException("Only PDF and Word documents are allowed.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "CVs");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{therapistId}_{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await cvFile.CopyToAsync(stream);

        return $"/Uploads/CVs/{fileName}";
    }

    public async Task CompleteProfileAsync(CompleteProfileRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Specialization))
            throw new ArgumentException("Specialization is required.");

        await Task.CompletedTask;
    }

    public async Task AddAvailabilityAsync(AddAvailabilityRequest request)
    {
        if (request.Slots is null || request.Slots.Count == 0)
            throw new ArgumentException("At least one availability slot is required.");

        await Task.CompletedTask;
    }

    public async Task SubmitCriteriaAsync(SubmitCriteriaRequest request)
    {
        var submittedTypes = request.Documents
            .Select(d => d.DocumentType)
            .ToList();

        var errors = TherapistCriteriaValidator.Validate(request.Category, submittedTypes);

        if (errors.Any())
            throw new ArgumentException(string.Join(" | ", errors));

        var documents = request.Documents.Select(d => new TherapistDocument
        {
            TherapistId = request.TherapistId,
            DocumentType = d.DocumentType,
            FileUrl = d.FileUrl,
            Notes = d.Notes
        }).ToList();

        await Task.CompletedTask;
    }

    public async Task UpdatePersonalInfoAsync(UpdatePersonalInfoRequest request)
    {
        var info = request.PersonalInfo;

        var age = DateTime.UtcNow.Year - info.DateOfBirth.Year;
        if (info.DateOfBirth.Date > DateTime.UtcNow.AddYears(-age)) age--;
        if (age < 18)
            throw new ArgumentException("Therapist must be at least 18 years old.");

        if (info.Languages is null || info.Languages.Count == 0)
            throw new ArgumentException("At least one language must be selected.");

        if (info.Languages.Distinct().Count() != info.Languages.Count)
            throw new ArgumentException("Duplicate languages are not allowed.");

        await Task.CompletedTask;
    }
}