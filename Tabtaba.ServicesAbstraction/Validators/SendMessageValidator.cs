using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.ServicesAbstraction.Validators;

public class SendMessageValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageValidator()
    {
        RuleFor(x => x.ReceiverId)
            .NotEmpty()
            .WithMessage("Receiver is required.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Message cannot be empty.")
            .MaximumLength(1000)
            .WithMessage("Message cannot exceed 1000 characters.");
    }
}