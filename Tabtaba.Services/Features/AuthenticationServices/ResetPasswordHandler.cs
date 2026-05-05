using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.ServicesAbstraction.Interfaces;
using Tabtaba.Shared.DTOs.Auth;

namespace Tabtaba.Services.Features.AuthenticationServices;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public ResetPasswordHandler(
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<ResetPasswordResponse> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // ── Find User ──────────────────────────────────────────────
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new ResetPasswordResponse
            {
                Success = false,
                Message = "User not found."
            };

        // ── Check New Password != Old Password ─────────────────────
        var isSamePassword = await _userManager.CheckPasswordAsync(
            user, request.NewPassword);

        if (isSamePassword)
            return new ResetPasswordResponse
            {
                Success = false,
                Message = "New password must be different from the previously used password."
            };

        // ── Reset Password ─────────────────────────────────────────
        var result = await _userManager.ResetPasswordAsync(
            user,
            request.ResetToken,
            request.NewPassword);

        if (!result.Succeeded)
            return new ResetPasswordResponse
            {
                Success = false,
                Message = result.Errors.FirstOrDefault()?.Description ?? "Failed to reset password."
            };

        // ── Send Confirmation Email ────────────────────────────────
        await _emailService.SendEmailAsync(
            user.Email!,
            "Tabtaba - Password Changed Successfully",
            @"<h2>Password Changed</h2>
              <p>Your password has been changed successfully.</p>
              <p>If you didn't do this, contact support immediately.</p>"
        );

        return new ResetPasswordResponse
        {
            Success = true,
            Message = "Password reset successfully."
        };
    }
}