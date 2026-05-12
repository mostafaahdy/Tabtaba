using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.Schedule;

namespace Tabtaba.ServicesAbstraction.Queries;

public record GetWeeklyScheduleQuery(
    string DoctorId,
    DateTime WeekStartDate
) : IRequest<WeeklyScheduleResponse>;
