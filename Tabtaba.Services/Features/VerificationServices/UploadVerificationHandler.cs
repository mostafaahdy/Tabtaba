using MediatR;
using Microsoft.AspNetCore.Http;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Enums;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.Services.Features.VerificationServices;

public class UploadVerificationHandler : IRequestHandler<UploadVerificationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UploadVerificationHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(
        UploadVerificationCommand request,
        CancellationToken cancellationToken)
    {
        
        var therapists = await _unitOfWork
            .GetRepository<Therapist>()
            .GetAllAsync();

        var therapist = therapists.FirstOrDefault(t => t.Id == request.TherapistId);

        if (therapist is null)
            throw new KeyNotFoundException($"Therapist {request.TherapistId} not found.");

        
        var cvUrl = await SaveFileAsync(request.CvFile, "CVs");

        var cvDocument = new TherapistDocument
        {
            TherapistId = request.TherapistId,
            DocumentType = DocumentType.CV,
            FileUrl = cvUrl,
            OriginalFileName = request.CvFile.FileName,
            FileSizeInBytes = request.CvFile.Length
        };

        await _unitOfWork
            .GetRepository<TherapistDocument>()
            .AddAsync(cvDocument);

       
        foreach (var certFile in request.CertificateFiles)
        {
            var certUrl = await SaveFileAsync(certFile, "Certificates");

            var certDocument = new TherapistDocument
            {
                TherapistId = request.TherapistId,
                DocumentType = DocumentType.CertifiedCertificate,
                FileUrl = certUrl,
                OriginalFileName = certFile.FileName,
                FileSizeInBytes = certFile.Length
            };

            await _unitOfWork
                .GetRepository<TherapistDocument>()
                .AddAsync(certDocument);
        }

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    
    private static async Task<string> SaveFileAsync(IFormFile file, string folder)
    {
        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(), "Uploads", folder);

        Directory.CreateDirectory(uploadsFolder);

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/Uploads/{folder}/{fileName}";
    }
}