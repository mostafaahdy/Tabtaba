using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tabtaba.ServicesAbstraction.Validators
{
    public class CreateAppointmentValidator :AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentValidator()
        {
            
            RuleFor(x => x.Duration_Minutes)
                .InclusiveBetween(15,120)
                .WithMessage("Session duration must be between 15 and 120 minutes");

           
            RuleFor(x => x.Date_Time)
                .NotEmpty().WithMessage("Session date and time must be specified")
                .GreaterThan(DateTime.UtcNow).WithMessage("You cannot book a session in the past");

            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.PatientId).NotEmpty();
        }
    }
}
