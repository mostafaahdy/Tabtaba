using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record UpdateNotificationPreferencesCommand(
     bool SessionReminders,
     bool MoodTrackingUpdates,
     bool TherapistMessages,
     bool NewContentAlerts,
     bool PersonalizedTips
 ) :IRequest<bool>;
}
