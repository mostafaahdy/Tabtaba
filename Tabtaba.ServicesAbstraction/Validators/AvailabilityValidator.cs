using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class AvailabilityValidator : AbstractValidator<SaveAvailabilityCommand>
{
    public AvailabilityValidator()
    {
        RuleFor(x => x.TherapistId)
            .NotEmpty()
            .WithMessage("Therapist ID is required.");

        RuleFor(x => x.DaysAvailability)
            .NotEmpty()
            .WithMessage("At least one day must be provided.");

        
        RuleForEach(x => x.DaysAvailability)
            .ChildRules(day =>
            {
                day.When(d => d.IsAvailable, () =>
                {
                    day.RuleFor(d => d.FromTime)
                        .NotNull()
                        .WithMessage("From time is required when day is available.");

                    day.RuleFor(d => d.ToTime)
                        .NotNull()
                        .WithMessage("To time is required when day is available.");

                    day.RuleFor(d => d)
                        .Must(d => d.ToTime > d.FromTime)
                        .WithMessage("To time must be after From time.");
                });
            });
    }
}