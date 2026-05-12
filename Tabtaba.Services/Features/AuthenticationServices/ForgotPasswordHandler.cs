using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.ServicesAbstraction.Interfaces;
using Tabtaba.Shared.Auth;

namespace Tabtaba.Services.Features.AuthenticationServices;

public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public ForgotPasswordHandler(
        UserManager<User> userManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<ForgotPasswordResponse> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // ── Find User ──────────────────────────────────────────────
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || user.Phone != request.MobileNumber)
            return new ForgotPasswordResponse
            {
                Success = false,
                Message = "No account found with this email and mobile number."
            };

        // ── Generate Confirmation Code ─────────────────────────────
        var confirmationCode = new Random().Next(100000, 999999).ToString();

        // ── Save Code ──────────────────────────────────────────────
        await _userManager.SetAuthenticationTokenAsync(
            user,
            "PasswordReset",
            "ConfirmationCode",
            confirmationCode);

        // ── Send Email ─────────────────────────────────────────────
        await _emailService.SendEmailAsync(
            user.Email!,
            "Tabtaba - Password Reset Code",
            $@"<h2>Password Reset</h2>
               <p>Your confirmation code is: <strong>{confirmationCode}</strong></p>
               <p>This code will expire in 15 minutes.</p>"
        );

        return new ForgotPasswordResponse
        {
            Success = true,
            Message = "Confirmation code sent to your email."
        };
    }
}