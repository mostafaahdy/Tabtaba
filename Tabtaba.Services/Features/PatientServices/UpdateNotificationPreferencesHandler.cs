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
    public class UpdateNotificationPreferencesHandler :IRequestHandler<UpdateNotificationPreferencesCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateNotificationPreferencesHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(UpdateNotificationPreferencesCommand request,CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _unitOfWork.GetRepository<Patient>().GetByIdAsync(userId!);

            if( user == null ) return false;

            user.SessionReminders = request.SessionReminders;
            user.MoodTrackingUpdates = request.MoodTrackingUpdates;
            user.TherapistMessages = request.TherapistMessages;
            user.NewContentAlerts = request.NewContentAlerts;
            user.PersonalizedTips = request.PersonalizedTips;

            _unitOfWork.GetRepository<Patient>().Update(user);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}