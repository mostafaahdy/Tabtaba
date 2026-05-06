using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class RegisterTherapistValidator : AbstractValidator<RegisterTherapistCommand>
{
    public RegisterTherapistValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.L_Name).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
        RuleFor(x => x.Phone).NotEmpty().Matches(@"^01[0125]\d{8}$").WithMessage("Invalid Egyptian phone number.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.");
        RuleFor(x => x.Specialization).NotEmpty().WithMessage("Specialization is required.");
        RuleFor(x => x.YearsOfExperience).GreaterThan(0).WithMessage("Years of experience must be greater than 0.");
        RuleFor(x => x.LicenseNumber).NotEmpty().WithMessage("License number is required.");
    }
}