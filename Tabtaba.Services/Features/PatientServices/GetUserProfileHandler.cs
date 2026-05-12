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
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.User;

namespace Tabtaba.Services.Features.PatientServices
{
    public class GetUserProfileHandler :IRequestHandler<GetUserProfileQuery,PatientProfileResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserProfileHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PatientProfileResponse> Handle(GetUserProfileQuery request,CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId!);

            if( user == null ) return new PatientProfileResponse();

            return new PatientProfileResponse
            {
                FullName = user.FullName ?? "",
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                DateOfBirth = user.DateOfBirth,
                ProfileImageUrl = user.ImageUrl
            };
        }
    }
    
}