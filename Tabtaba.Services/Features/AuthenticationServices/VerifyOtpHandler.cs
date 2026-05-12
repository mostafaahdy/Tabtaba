using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.Auth;

namespace Tabtaba.Services.Features.AuthenticationServices;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, VerifyOtpResponse>
{
    private readonly UserManager<User> _userManager;

    public VerifyOtpHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<VerifyOtpResponse> Handle(
        VerifyOtpCommand request,
        CancellationToken cancellationToken)
    {
        // ── Find User ──────────────────────────────────────────────
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new VerifyOtpResponse
            {
                Success = false,
                Message = "User not found."
            };

        // ── Get Saved Code ─────────────────────────────────────────
        var savedCode = await _userManager.GetAuthenticationTokenAsync(
            user,
            "PasswordReset",
            "ConfirmationCode");

        if (savedCode is null || savedCode != request.Code)
            return new VerifyOtpResponse
            {
                Success = false,
                Message = "Invalid or expired OTP code."
            };

        // ── Generate Reset Token ───────────────────────────────────
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        // ── Remove Used Code ───────────────────────────────────────
        await _userManager.RemoveAuthenticationTokenAsync(
            user,
            "PasswordReset",
            "ConfirmationCode");

        return new VerifyOtpResponse
        {
            Success = true,
            Message = "OTP verified successfully.",
            ResetToken = resetToken
        };
    }
}