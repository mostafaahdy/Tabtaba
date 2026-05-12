using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Session;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
    public record GetSessionReminderQuery(int PatientId) :IRequest<SessionReminderResponse>;
}
