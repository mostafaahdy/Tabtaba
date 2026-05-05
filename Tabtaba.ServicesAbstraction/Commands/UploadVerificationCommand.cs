using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tabtaba.ServicesAbstraction.Commands;

public record UploadVerificationCommand(
    Guid TherapistId,
    IFormFile CvFile,
    List<IFormFile> CertificateFiles
) : IRequest<bool>;