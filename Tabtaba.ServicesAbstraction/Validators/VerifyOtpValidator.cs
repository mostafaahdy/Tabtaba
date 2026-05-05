using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("OTP code is required.")
            .Length(4, 6)
            .WithMessage("OTP code must be 4-6 digits.")
            .Matches(@"^\d+$")
            .WithMessage("OTP code must be numbers only.");
    }
}