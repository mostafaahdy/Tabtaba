using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class ProfessionalInfoValidator : AbstractValidator<SaveProfessionalInfoCommand>
{
    public ProfessionalInfoValidator()
    {
        RuleFor(x => x.TherapistId)
            .NotEmpty()
            .WithMessage("Therapist ID is required.");

        RuleFor(x => x.ProfessionalCategory)
            .NotEmpty()
            .WithMessage("Professional category is required.")
            .MaximumLength(100)
            .WithMessage("Category must not exceed 100 characters.");

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .WithMessage("Specialization is required.")
            .MaximumLength(200)
            .WithMessage("Specialization must not exceed 200 characters.");

        RuleFor(x => x.YearsOfExperience)
            .InclusiveBetween(0, 60)
            .WithMessage("Years of experience must be between 0 and 60.");

        RuleFor(x => x.LicenseNumber)
            .MaximumLength(50)
            .WithMessage("License number must not exceed 50 characters.")
            .When(x => x.LicenseNumber is not null);
    }
}