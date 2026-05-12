using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.Services.Specifications;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.KnowledgeLibrary;

namespace Tabtaba.Services.Features.KnowledgeLibraryServices
{
    public class ToggleLikeHandler :IRequestHandler<ToggleLikeCommand,string>
    {
            private readonly IUnitOfWork _unitOfWork;

            public ToggleLikeHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<string> Handle(ToggleLikeCommand request,CancellationToken cancellationToken)
            {
                var repository = _unitOfWork.GetRepository<KnowledgeLike>();
                var existingLike = await repository.GetByIdAsync(request.UserId,request.KnowledgeId);

                if( existingLike == null )
                {
                    var newLike = new KnowledgeLike
                    {
                        UserId = request.UserId,
                        KnowledgeId = request.KnowledgeId,
                        LikedAt = DateTime.UtcNow
                    };

                    await repository.AddAsync(newLike);
                    await _unitOfWork.SaveChangesAsync();

                    return "Liked"; 
                }
                else
                {
                    repository.Remove(existingLike);
                    await _unitOfWork.SaveChangesAsync();

                    return "Unliked"; 
                }
            }
        


        #region Helper Method
        private string FormatViews(int views)
        {
            if( views >= 1000000 )
                return (views / 1000000D).ToString("0.#M");
            if( views >= 1000 )
                return (views / 1000D).ToString("0.#K");

            return views.ToString();
        } 
        #endregion
    }
}
