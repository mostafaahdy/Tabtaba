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
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.User;

namespace Tabtaba.Services.Features.PatientServices
{
    public class UpdatePrivacySettingsHandler :IRequestHandler<UpdatePrivacySettingsCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdatePrivacySettingsHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(UpdatePrivacySettingsCommand request,CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if( string.IsNullOrEmpty(userId) ) return false;

            var userRepo = _unitOfWork.GetRepository<Patient>();
            var user = await userRepo.GetByIdAsync(userId);

            if( user == null ) return false;

            user.ReceiveEmails = request.ReceiveEmails;
            user.ReceiveNotifications = request.ReceiveNotifications;

            userRepo.Update(user);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
    
}
