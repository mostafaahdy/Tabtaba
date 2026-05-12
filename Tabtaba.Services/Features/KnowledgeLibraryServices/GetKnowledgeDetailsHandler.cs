using MediatR;
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
    public class GetKnowledgeDetailsHandler :IRequestHandler<GetKnowledgeDetailsQuery,KnowledgeDetailsDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetKnowledgeDetailsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<KnowledgeDetailsDTO> Handle(GetKnowledgeDetailsQuery request,CancellationToken cancellationToken)
        {
           
            var repository = _unitOfWork.GetRepository<KnowledgeLibrary>();
            var entity = await repository.GetByIdAsync(request.Id);

            if( entity == null ) return null!;

           
            return new KnowledgeDetailsDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                VideoUrl = entity.VideoUrl!,
                AuthorName = entity.Doctor?.User?.FullName ?? "Tabtaba Team",

                ShareUrl = $"https://tabtaba.app/content/{entity.Id}",

                
                Chapters = entity.Chapters?.Select(c => new VideoChapterDTO
                {
                    Title = c.Title,
                    Timestamp = c.Timestamp
                }).OrderBy(c => c.Timestamp).ToList() ?? new(),

                
                Comments = entity.Comments?.Select(com => new CommentDTO
                {
                    Id = com.Id,
                    UserName = com.User.FullName,
                    UserImageUrl = com.User.ImageUrl!,
                    Content = com.Content,
                    CreatedAt = com.CreatedAt.ToString("yyyy-MM-dd")
                }).OrderByDescending(com => com.Id).ToList() ?? new(),

                LikesCount = entity.Likes?.Count() ?? 0,
                IsLiked = entity.Likes?.Any(l => l.UserId == request.UserId) ?? false,
                IsSaved = entity.SavedByUsers?.Any(s => s.UserId == request.UserId) ?? false
            };

        }
    }
}
