using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public class IncrementViewsCommand :IRequest<bool>
    {
        public int Id { get; set; }
        public IncrementViewsCommand(int id) => Id = id;
    }
    
}
