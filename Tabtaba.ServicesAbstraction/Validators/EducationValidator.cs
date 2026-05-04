using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class EducationValidator : AbstractValidator<SaveEducationCommand>
{
    public EducationValidator()
    {
        RuleFor(x => x.TherapistId)
            .NotEmpty()
            .WithMessage("Therapist ID is required.");

        RuleFor(x => x.HighestDegree)
            .NotEmpty()
            .WithMessage("Highest degree is required.")
            .MaximumLength(100)
            .WithMessage("Highest degree must not exceed 100 characters.");

        RuleFor(x => x.GraduationYear)
            .NotEmpty()
            .WithMessage("Graduation year is required.")
            .InclusiveBetween(1950, DateTime.UtcNow.Year)
            .WithMessage($"Graduation year must be between 1950 and {DateTime.UtcNow.Year}.");

        RuleFor(x => x.UniversityName)
            .NotEmpty()
            .WithMessage("University name is required.")
            .MaximumLength(200)
            .WithMessage("University name must not exceed 200 characters.");
    }
}