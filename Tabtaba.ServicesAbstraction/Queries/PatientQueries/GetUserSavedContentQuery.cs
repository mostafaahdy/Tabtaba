using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.KnowledgeLibrary;

namespace Tabtaba.ServicesAbstraction.Queries.PatientQueries
{
        public record GetUserSavedContentQuery(
        string UserId,
        int PageNumber = 1,
        int PageSize = 10
       ) :IRequest<KnowledgeLibraryResponse>;
    

}
