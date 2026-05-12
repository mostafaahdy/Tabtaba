using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Doctor;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
    public record GetDoctorReportQuery(int PatientId) :IRequest<DoctorReportResponse>;
}
