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
using Tabtaba.Shared.User;

namespace Tabtaba.Services.Features.PatientServices
{
    public class UserSettingsHandler :IRequestHandler<GetUserSettingsQuery,UserSettingsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSettingsHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserSettingsResponse> Handle(GetUserSettingsQuery request,CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _unitOfWork.GetRepository<Patient>().GetByIdAsync(userId!);

            if( user == null ) return new UserSettingsResponse();

            return new UserSettingsResponse
            {
                EnableNotifications = user.EnableNotifications, 
                MoodTrackingReminders = user.MoodTrackingReminders,
                DarkMode = user.DarkMode,
                Language = user.Language ?? "ar"
            };
        }
    }
    
}
