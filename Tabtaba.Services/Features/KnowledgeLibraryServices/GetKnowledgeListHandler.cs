using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.Services.Specifications;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.KnowledgeLibrary;

namespace Tabtaba.Services.Features.KnowledgeLibraryServices
{
    public class GetKnowledgeListHandler :IRequestHandler<GetKnowledgeListQuery,KnowledgeLibraryResponse>
    {

        private readonly IUnitOfWork _unitOfWork;


        public GetKnowledgeListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

            public async Task<KnowledgeLibraryResponse> Handle(GetKnowledgeListQuery request,CancellationToken cancellationToken)
            {
            var spec = new KnowledgeLibraryWithFiltersSpecification(request.ContentType,request.Category);

            var allEntities = await _unitOfWork.GetRepository<KnowledgeLibrary>().ListAsync(spec);
            var totalItems = allEntities.Count;
            var pagedEntities = allEntities
                    .Skip((request.PageNumber - 1) * request.PageSize) 
                    .Take(request.PageSize)                            
                    .ToList();

            var dtos = pagedEntities.Select(e => new KnowledgeItemDTO
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                Category = e.Category,
                ContentType = e.ContentType,

                AuthorName = e.Doctor?.User?.FullName ?? "Tabtaba",
                AuthorImageUrl = e.Doctor?.User?.ImageUrl ?? "",

                ReadTime = e.ContentType == "Article" ? $"{e.ReadTimeMinutes} min read" : "Video Content",
                ViewsCount = e.ViewsCount > 1000 ? $"{(e.ViewsCount / 1000.0):F1}K views" : $"{e.ViewsCount} views",
                CreatedAt = e.CreatedAt.ToString("MMM dd, yyyy")
            }).ToList();

            return new KnowledgeLibraryResponse
            {
                KnowledgeItems = dtos,
                TotalCount = totalItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int) Math.Ceiling(totalItems / (double) request.PageSize)
            };
        }
    }
    
}
