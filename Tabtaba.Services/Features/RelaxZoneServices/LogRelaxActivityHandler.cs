using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;

namespace Tabtaba.Services.Features.RelaxZoneServices
{
    public class LogRelaxActivityHandler :IRequestHandler<LogRelaxActivityCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogRelaxActivityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(LogRelaxActivityCommand request,CancellationToken cancellationToken)
        {
            var activityLog = new RelaxLog
            {
                PatientId = request.PatientId,       
                RelaxContentId = request.RelaxContentId,
                CompletedAt = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<RelaxLog>().AddAsync(activityLog);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
