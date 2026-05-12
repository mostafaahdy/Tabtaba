using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.KnowledgeLibrary;

namespace Tabtaba.Services.Features.PatientServices
{
    public class GetSavedContentHandler :IRequestHandler<GetSavedContentQuery,List<SavedContentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetSavedContentHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SavedContentResponse>> Handle(GetSavedContentQuery request,CancellationToken cancellationToken)
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int.TryParse(userIdStr,out int patientId);

          
            var savedItems = await _unitOfWork.GetRepository<UserSavedContent>()
                .GetQueryable()
                .Include(s => s.KnowledgeLibrary) 
                .Where(s => s.PatientId == patientId)
                .OrderByDescending(s => s.SavedAt)
                .ToListAsync(cancellationToken);

            return savedItems.Select(item => new SavedContentResponse
            {
                Id = item.KnowledgeLibraryId,
                Title = item.KnowledgeLibrary.Title,
                ImageUrl = item.KnowledgeLibrary.ImageUrl,
                Category = item.KnowledgeLibrary.Category,
                ContentType = item.KnowledgeLibrary.ContentType,
                Views = item.KnowledgeLibrary.ViewsCount,
                SavedAt = item.SavedAt
            }).ToList();
        }
    }
    
}
