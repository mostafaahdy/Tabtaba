using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public class ToggleLikeCommand :IRequest<string>
    {
        public int KnowledgeId { get; set; }
        public string UserId { get; set; }

        public ToggleLikeCommand(int knowledgeId,string userId)
        {
            KnowledgeId = knowledgeId;
            UserId = userId;
            
        }
    }
    
}
