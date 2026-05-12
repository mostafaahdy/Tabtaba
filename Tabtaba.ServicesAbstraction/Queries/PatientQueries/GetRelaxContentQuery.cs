using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.RelaxZone;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
    public class GetRelaxContentQuery :IRequest<List<RelaxContentDTO>>
    {
        public string Category { get; set; }
        public GetRelaxContentQuery(string category) => Category = category;
    }

}
