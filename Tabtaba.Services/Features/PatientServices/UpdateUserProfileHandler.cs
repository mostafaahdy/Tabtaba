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
    public class UpdateUserProfileHandler :IRequestHandler<UpdateUserProfileCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserProfileHandler(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(UpdateUserProfileCommand request,CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if( string.IsNullOrEmpty(userId) )
            {
                return false; 
            }
            var userRepo = _unitOfWork.GetRepository<User>();
            var user = await userRepo.GetByIdAsync(userId); 

            if( user == null ) return false;

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.ImageUrl = request.ProfileImageUrl;
            if( request.DateOfBirth.HasValue )
            {
                user.DateOfBirth = request.DateOfBirth.Value;
            }
            userRepo.Update(user);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
