using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;

namespace Tabtaba.ServicesAbstraction.Validators
{
    public class AddMoodLogValidator
    :AbstractValidator<AddMoodLogCommand>
    {
        public AddMoodLogValidator()
        {
            RuleFor(x => x.Status).IsInEnum().WithMessage("You must select an existing mood state");
            RuleFor(x => x.Note).MaximumLength(500).WithMessage("The note is too long");
        }
    }
}
