using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;

namespace Tabtaba.Services.Features.PatientServices
{
    public class AddMoodLogHandler :IRequestHandler<AddMoodLogCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddMoodLogHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(AddMoodLogCommand request,CancellationToken cancellationToken)
        {
           
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var moodLog = new MoodLog
            {
                Status = request.Status,
                Note = request.Note,
                PatientId = userId ?? "", 
                Date = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<MoodLog>().AddAsync(moodLog);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
    
}
