using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tabtaba.ServicesAbstraction.Commands
{
    // عدلنا الـ int وخليناها string عشان تستقبل الـ GUID صح من غير كراش
    public record LogoutCommand(string UserId) : IRequest<bool>;
}