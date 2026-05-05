using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class SaveSessionNoteValidator : AbstractValidator<SaveSessionNoteCommand>
{
    public SaveSessionNoteValidator()
    {
        RuleFor(x => x.AppointmentId)
            .GreaterThan(0)
            .WithMessage("Appointment ID is required.");

        RuleFor(x => x.Notes)
            .NotEmpty()
            .WithMessage("Notes cannot be empty.")
            .MaximumLength(2000)
            .WithMessage("Notes cannot exceed 2000 characters.");
    }
}
