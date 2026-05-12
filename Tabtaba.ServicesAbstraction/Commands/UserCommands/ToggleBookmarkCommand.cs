using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public  class ToggleBookmarkCommand :IRequest<string>
    {
        public int ContentId { get; set; }
        public string UserId { get; set; } 

        public ToggleBookmarkCommand(int contentId,string userId)
        {
            ContentId = contentId;
            UserId = userId;
        }
    }
}
