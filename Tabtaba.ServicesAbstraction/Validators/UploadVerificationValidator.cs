using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class UploadVerificationValidator : AbstractValidator<UploadVerificationCommand>
{
    private static readonly string[] AllowedCvExtensions = { ".pdf", ".doc", ".docx" };
    private static readonly string[] AllowedCertExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };
    private const long MaxCvSizeInBytes = 5 * 1024 * 1024; // 5MB

    public UploadVerificationValidator()
    {
        RuleFor(x => x.TherapistId)
            .NotEmpty()
            .WithMessage("Therapist ID is required.");

        
        RuleFor(x => x.CvFile)
            .NotNull()
            .WithMessage("CV file is required.")
            .Must(file => file.Length <= MaxCvSizeInBytes)
            .WithMessage("CV file must not exceed 5MB.")
            .Must(file => AllowedCvExtensions.Contains(
                Path.GetExtension(file.FileName).ToLowerInvariant()))
            .WithMessage("CV must be PDF or DOC format.");

       
        RuleFor(x => x.CertificateFiles)
            .NotEmpty()
            .WithMessage("At least one certificate is required.");

        RuleForEach(x => x.CertificateFiles)
            .Must(file => AllowedCertExtensions.Contains(
                Path.GetExtension(file.FileName).ToLowerInvariant()))
            .WithMessage("Certificates must be PDF, JPG, or PNG format.");
    }
}