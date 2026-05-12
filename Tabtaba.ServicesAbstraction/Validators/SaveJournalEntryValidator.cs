using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;

namespace Tabtaba.ServicesAbstraction.Validators
{
    public class SaveJournalEntryValidator :AbstractValidator<SaveJournalEntryCommand>
    {

    public SaveJournalEntryValidator()
        {
            RuleFor(x => x.Note)
                .NotEmpty().WithMessage("You cannot save an empty note.")
                .MinimumLength(5).WithMessage("Please write more details about your day (at least 5 characters).");

            RuleFor(x => x.MoodScore)
                .InclusiveBetween(1,10);
        }
    }
}

