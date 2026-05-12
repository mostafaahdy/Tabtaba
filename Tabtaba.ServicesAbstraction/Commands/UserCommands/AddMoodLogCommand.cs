using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.Enums;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record AddMoodLogCommand(
     MoodStatus Status,
     string? Note
 ) :IRequest<bool>;
}
