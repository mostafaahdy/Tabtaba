using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;

namespace Tabtaba.Services.Features.KnowledgeLibraryServices
{
    public class ToggleBookmarkHandler :IRequestHandler<ToggleBookmarkCommand,string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToggleBookmarkHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<string> Handle(ToggleBookmarkCommand request,CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<UserSavedContent>();

            
            var existingBookmark = await repository.FirstOrDefaultAsync(s =>
            s.UserId == request.UserId && s.KnowledgeLibraryId == request.ContentId);

            if( existingBookmark == null )
            {
               
                var bookmark = new UserSavedContent
                {
                    UserId = request.UserId,
                    KnowledgeLibraryId = request.ContentId
                };
                await repository.AddAsync(bookmark);
                await _unitOfWork.SaveChangesAsync();
                return "Saved successfully";
            }
            else
            {
               
                repository.Remove(existingBookmark);
                await _unitOfWork.SaveChangesAsync();
                return "Removed from bookmarks";
            }
        }

    }
}
