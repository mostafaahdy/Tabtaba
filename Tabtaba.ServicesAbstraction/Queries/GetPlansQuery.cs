using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Tabtaba.Shared.Checkout;

namespace Tabtaba.ServicesAbstraction.Queries;

public record GetPlansQuery : IRequest<List<PlanResponse>>;