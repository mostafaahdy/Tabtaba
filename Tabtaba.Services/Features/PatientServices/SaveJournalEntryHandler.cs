using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;

namespace Tabtaba.Services.Features.PatientServices
{
    public class SaveJournalEntryHandler :IRequestHandler<SaveJournalEntryCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaveJournalEntryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(SaveJournalEntryCommand request,CancellationToken cancellationToken)
        {
            var entry = new MoodLog
            {
                PatientId = request.PatientId,
                Note = request.Note, 
                Status = (MoodStatus) request.MoodScore, 
                Date = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<MoodLog>().AddAsync(entry);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}

