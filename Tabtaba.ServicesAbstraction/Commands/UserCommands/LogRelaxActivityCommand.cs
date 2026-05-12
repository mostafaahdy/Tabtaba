using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public class LogRelaxActivityCommand :IRequest<bool>
    {
        public string PatientId { get; set; } = default!;
        public int RelaxContentId { get; set; }
    }

}
