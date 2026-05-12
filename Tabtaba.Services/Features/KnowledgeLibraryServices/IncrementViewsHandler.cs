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
    public class IncrementViewsHandler :IRequestHandler<IncrementViewsCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncrementViewsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(IncrementViewsCommand request,CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<KnowledgeLibrary>();
            var content = await repository.GetByIdAsync(request.Id);

            if( content == null ) return false;

            content.ViewsCount++;

            repository.Update(content);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }
    }

}
