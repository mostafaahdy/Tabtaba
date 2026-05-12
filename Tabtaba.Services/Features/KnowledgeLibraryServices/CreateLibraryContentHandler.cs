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
    public class CreateLibraryContentHandler :IRequestHandler<CreateLibraryContentCommand,int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateLibraryContentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(CreateLibraryContentCommand request,CancellationToken cancellationToken)
        {
            var content = new KnowledgeLibrary
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                VideoUrl = request.VideoUrl,
                Category = request.Category,
                ContentType = request.ContentType,
                ReadTimeMinutes = request.ReadTimeMinutes,
                DoctorId = request.DoctorId,
                ViewsCount = 0, 
                CreatedAt = DateTime.UtcNow
            };

           
            await _unitOfWork.GetRepository<KnowledgeLibrary>().AddAsync(content);

           
            await _unitOfWork.SaveChangesAsync();

            return content.Id; 
        }
    }
}

