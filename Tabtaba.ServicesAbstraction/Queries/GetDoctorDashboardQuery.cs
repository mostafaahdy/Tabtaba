using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Doctor;

namespace Tabtaba.ServicesAbstraction.Queries;

public record GetDoctorDashboardQuery(string DoctorId) : IRequest<DoctorDashboardResponse>;
