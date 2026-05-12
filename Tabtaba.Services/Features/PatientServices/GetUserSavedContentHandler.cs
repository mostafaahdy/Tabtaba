using MediatR;
using Microsoft.EntityFrameworkCore.Query;
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

namespace Tabtaba.Services.Features.PatientServices
{
    public class GetUserSavedContentHandler :IRequestHandler<GetUserSavedContentQuery,KnowledgeLibraryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserSavedContentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<KnowledgeLibraryResponse> Handle(GetUserSavedContentQuery request,CancellationToken cancellationToken)
        {

            var spec = new UserSavedContentWithEntriesSpecification(request.UserId);

           
            var savedItems = await _unitOfWork.GetRepository<UserSavedContent>().ListAsync(spec);
            var totalItems = savedItems.Count;
            var pagedSavedItems = savedItems
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = pagedSavedItems.Select(s => new KnowledgeItemDTO
            {
                Id = s.KnowledgeLibrary.Id,
                Title = s.KnowledgeLibrary.Title,
                Description = s.KnowledgeLibrary.Description,
                ImageUrl = s.KnowledgeLibrary.ImageUrl,
                Category = s.KnowledgeLibrary.Category,
                ContentType = s.KnowledgeLibrary.ContentType,
                AuthorName = s.KnowledgeLibrary.Doctor?.User?.FullName ?? "Tabtaba",
                AuthorImageUrl = s.KnowledgeLibrary.Doctor?.User?.ImageUrl ?? "",
                ReadTime = s.KnowledgeLibrary.ContentType == "Article"
                           ? $"{s.KnowledgeLibrary.ReadTimeMinutes} min read" : "Video",
                CreatedAt = s.KnowledgeLibrary.CreatedAt.ToString("MMM dd, yyyy")
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
