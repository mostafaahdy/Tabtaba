using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.Services.Features.AuthenticationServices
{
    public class LogoutHandler :IRequestHandler<LogoutCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogoutHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(LogoutCommand request,CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.GetRepository<User>();
            var user = await userRepo.GetByIdAsync(request.UserId);

            if( user == null ) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            userRepo.Update(user);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
