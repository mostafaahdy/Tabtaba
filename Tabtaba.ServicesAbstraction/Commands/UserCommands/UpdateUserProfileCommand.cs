using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record UpdateUserProfileCommand(
    string FullName,
    string PhoneNumber,
    string Email,
    DateTime? DateOfBirth,
    string? ProfileImageUrl
) :IRequest<bool>;
}
