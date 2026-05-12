using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Stats;

namespace Tabtaba.ServicesAbstraction.Queries
{
    public record GetMoodAnalyticsQuery :IRequest<MoodAnalyticsResponse>;
}
