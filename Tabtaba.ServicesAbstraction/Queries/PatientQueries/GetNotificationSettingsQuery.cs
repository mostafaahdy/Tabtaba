using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.User;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
    public record GetNotificationSettingsQuery :IRequest<NotificationSettingsResponse>;
}
