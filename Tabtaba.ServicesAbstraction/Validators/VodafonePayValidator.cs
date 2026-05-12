using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tabtaba.Shared.Payment;

namespace Tabtaba.ServicesAbstraction.Validators;

public class VodafonePayValidator : AbstractValidator<VodafonePayRequest>
{
    public VodafonePayValidator()
    {
        RuleFor(x => x.WalletPhoneNumber)
            .NotEmpty()
            .Matches(@"^01[0125]\d{8}$")
            .WithMessage("Invalid Egyptian phone number.");

        RuleFor(x => x.AppointmentId)
            .GreaterThan(0)
            .WithMessage("Invalid appointment.");

        RuleFor(x => x.PatientId)
            .GreaterThan(0)
            .WithMessage("Invalid patient.");
    }
}