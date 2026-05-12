using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.RelaxZone;

namespace Tabtaba.Services.Features.RelaxZoneServices
{
    public class GetRelaxContentHandler :IRequestHandler<GetRelaxContentQuery,List<RelaxContentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRelaxContentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<RelaxContentDTO>> Handle(GetRelaxContentQuery request,CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<RelaxContent>();

            // Fetching content based on category
            var contents = await repository.GetQueryable()
                .Where(c => c.Category == request.Category)
                .Select(c => new RelaxContentDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Category = c.Category,
                    DurationInMinutes = c.DurationInMinutes,
                    MediaUrl = c.MediaUrl
                })
                .ToListAsync(cancellationToken);

            return contents;
        }
    }
   
}
