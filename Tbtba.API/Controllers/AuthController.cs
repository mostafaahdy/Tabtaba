using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Auth;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
        => _mediator = mediator;

    // ── Login
    [HttpPost("login")]
    [EnableRateLimiting("LoginPolicy")] // ✅
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
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
            var command = new LoginCommand(
                request.Email,
                request.Password,
                request.AgreeToTerms);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    // ── Google Sign In
    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action("GoogleCallback", "Auth");
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        }, "Google");
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync(
            IdentityConstants.ExternalScheme);

        if (!result.Succeeded)
            return Unauthorized();

        var email = result.Principal!.FindFirstValue(ClaimTypes.Email);
        var name = result.Principal!.FindFirstValue(ClaimTypes.Name);

        var command = new GoogleLoginCommand(email!, name ?? string.Empty);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    // ── Select Role
    [HttpPost("select-role")]
    public IActionResult SelectRole([FromBody] SelectRoleRequest request)
    {
        if (!Enum.IsDefined(typeof(UserRole), request.Role))
            return BadRequest(new { message = "Invalid role selected." });

        var redirectInfo = request.Role switch
        {
            UserRole.Patient => new
            {
                role = nameof(UserRole.Patient),
                nextStep = "/api/auth/register/patient"
            },
            UserRole.Therapist => new
            {
                role = nameof(UserRole.Therapist),
                nextStep = "/api/auth/register/therapist"
            },
            _ => null
        };

        return Ok(new { message = "Role selected successfully.", data = redirectInfo });
    }

    // ── Forgot Password
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ForgotPasswordCommand(request.Email, request.MobileNumber);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = result.Message });
    }

    // ── Verify OTP
    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new VerifyOtpCommand(request.Email, request.Code);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = result.Message, resetToken = result.ResetToken });
    }

    // ── Resend OTP
    [HttpPost("resend-otp")]
    public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ForgotPasswordCommand(request.Email, request.MobileNumber);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = "OTP resent successfully." });
    }

    // ── Reset Password
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ResetPasswordCommand(
            request.Email,
            request.ResetToken,
            request.NewPassword,
            request.ConfirmPassword);

        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = result.Message });
    }

    // ── Register Patient
    [HttpPost("register/patient")]
    public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientRequest request)
    {
        try
        {
            var command = new RegisterPatientCommand(
                request.FullName,
                request.L_Name,
                request.Email,
                request.Phone,
                request.Password,
                request.Gender,
                request.DateOfBirth,
                request.MaritalStatus);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // ── Register Therapist
    [HttpPost("register/therapist")]
    public async Task<IActionResult> RegisterTherapist([FromBody] RegisterTherapistRequest request)
    {
        try
        {
            var command = new RegisterTherapistCommand(
                request.FullName,
                request.L_Name,
                request.Email,
                request.Phone,
                request.Password,
                request.Gender,
                request.Specialization,
                request.YearsOfExperience,
                request.LicenseNumber);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // ── Refresh Token
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var command = new RefreshTokenCommand(
                request.AccessToken,
                request.RefreshToken);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}