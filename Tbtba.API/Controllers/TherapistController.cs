using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tabtaba.Domain.Enums;
using Tabtaba.ServicesAbstraction;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Therapist;

namespace Tabtaba.Web.Controllers;

[ApiController]
[Route("api/therapist")]
public class TherapistController : ControllerBase
{
    private readonly ITherapistService _therapistService;
    private readonly IMediator _mediator;

    public TherapistController(
        ITherapistService therapistService,
        IMediator mediator)
    {
        _therapistService = therapistService;
        _mediator = mediator;
    }
   

    [HttpPost("professional-info")]
    public async Task<IActionResult> SaveProfessionalInfo(
        [FromBody] ProfessionalInfoRequest request)
    {
        if (!ModelState.IsValid)
        {
            var validationErrors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { message = "Validation failed.", errors = validationErrors });
        }

        var command = new SaveProfessionalInfoCommand(
            request.TherapistId,
            request.ProfessionalCategory,
            request.Specialization,
            request.YearsOfExperience,
            request.LicensingAuthority,
            request.LicenseNumber
        );

        await _mediator.Send(command);

        return Ok(new
        {
            message = "Professional info saved successfully.",
            nextStep = "/api/therapist/submit-criteria"
        });
    }



    [HttpPost("upload-cv")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadCv([FromForm] UploadCvRequest request)
    {
        try
        {
            var cvUrl = await _therapistService.UploadCvAsync(request.TherapistId, request.CvFile);
            return Ok(new { message = "CV uploaded successfully.", cvUrl });
        }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPost("complete-profile")]
    public async Task<IActionResult> CompleteProfile([FromBody] CompleteProfileRequest request)
    {
        try
        {
            await _therapistService.CompleteProfileAsync(request);
            return Ok(new { message = "Profile completed successfully." });
        }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPost("add-availability")]
    public async Task<IActionResult> AddAvailability([FromBody] AddAvailabilityRequest request)
    {
        try
        {
            await _therapistService.AddAvailabilityAsync(request);
            return Ok(new { message = "Availability added successfully." });
        }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPost("submit-criteria")]
    public async Task<IActionResult> SubmitCriteria([FromBody] SubmitCriteriaRequest request)
    {
        try
        {
            await _therapistService.SubmitCriteriaAsync(request);
            return Ok(new { message = "Criteria submitted successfully. Pending admin review." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = "Validation failed.", errors = ex.Message.Split(" | ") });
        }
    }

    [HttpPost("availability")]
    public async Task<IActionResult> SaveAvailability(
    [FromBody] SaveAvailabilityRequest request)
    {
        if (!ModelState.IsValid)
        {
            var validationErrors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { message = "Validation failed.", errors = validationErrors });
        }

        var command = new SaveAvailabilityCommand(
            request.TherapistId,
            request.WorksAtClinic,
            request.DaysAvailability
        );

        await _mediator.Send(command);

        return Ok(new
        {
            message = "Availability saved successfully.",
            nextStep = "/api/therapist/submit-criteria"
        });
    }

    [HttpGet("required-documents/{category}")]
    public IActionResult GetRequiredDocuments(TherapistCategory category)
    {
        var docs = TherapistCriteriaValidator.GetRequiredDocuments(category);
        if (!docs.Any())
            return BadRequest(new { message = "Invalid category." });
        return Ok(new
        {
            category = category.ToString(),
            requiredDocuments = docs.Select(d => new
            {
                id = (int)d,
                name = d.ToString()
            })
        });
    }



    [HttpPost("verification")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadVerification(
    [FromForm] UploadVerificationRequest request)
    {
        if (!ModelState.IsValid)
        {
            var validationErrors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { message = "Validation failed.", errors = validationErrors });
        }

        var command = new UploadVerificationCommand(
            request.TherapistId,
            request.CvFile,
            request.CertificateFiles
        );

        await _mediator.Send(command);

        return Ok(new
        {
            message = "Verification documents uploaded successfully.",
            nextStep = "Registration complete. Pending admin review."
        });
    }

    [HttpPost("personal-info")]
    public async Task<IActionResult> UpdatePersonalInfo([FromBody] UpdatePersonalInfoRequest request)
    {
        if (!ModelState.IsValid)
        {
            var validationErrors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return BadRequest(new { message = "Validation failed.", errors = validationErrors });
        }

        try
        {
            await _therapistService.UpdatePersonalInfoAsync(request);
            return Ok(new { message = "Personal info saved successfully.", nextStep = "/api/therapist/upload-cv" });
        }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpPost("education")]
    public async Task<IActionResult> SaveEducation([FromBody] EducationRequest request)
    {
        var command = new SaveEducationCommand(
            request.TherapistId,
            request.HighestDegree,
            request.GraduationYear,
            request.UniversityName
        );

        await _mediator.Send(command);

        return Ok(new { message = "Education saved successfully.", nextStep = "/api/therapist/submit-criteria" });
    }
}
