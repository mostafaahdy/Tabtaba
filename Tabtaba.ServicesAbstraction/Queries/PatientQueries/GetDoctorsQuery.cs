using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Session;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
    public record GetDoctorsQuery(
    string? Specialization = null,
    string? Diagnosis = null,
    decimal? MinRate = null
) :IRequest<List<GetDoctorDTO>>;
}
