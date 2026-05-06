using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.ServicesAbstraction.Validators;

public class AddCardValidator : AbstractValidator<AddCardRequest>
{
    public AddCardValidator()
    {
        RuleFor(x => x.CardHolderName)
            .NotEmpty()
            .WithMessage("Card holder name is required.");

        RuleFor(x => x.CardNumber)
            .NotEmpty()
            .Length(16)
            .WithMessage("Card number must be 16 digits.")
            .Matches(@"^\d+$")
            .WithMessage("Card number must contain only digits.");

        RuleFor(x => x.ExpiryDate)
            .NotEmpty()
            .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$")
            .WithMessage("Expiry date must be in MM/YY format.");

        RuleFor(x => x.CVV)
            .NotEmpty()
            .Length(3, 4)
            .WithMessage("CVV must be 3 or 4 digits.")
            .Matches(@"^\d+$")
            .WithMessage("CVV must contain only digits.");
    }
}
