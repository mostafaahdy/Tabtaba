using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.KnowledgeLibrary;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
    public class GetKnowledgeDetailsQuery :IRequest<KnowledgeDetailsDTO>
    {
        public int Id { get; set; }
        public string? UserId { get; set; } 

        public GetKnowledgeDetailsQuery(int id,string? userId = null)
        {
            Id = id;
            UserId = userId;
        }
    }
}
