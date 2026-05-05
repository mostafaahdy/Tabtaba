using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Earnings;

namespace Tabtaba.ServicesAbstraction.Queries;

public record GetEarningsQuery(int DoctorId) : IRequest<EarningsResponse>;