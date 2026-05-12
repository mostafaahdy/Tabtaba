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

namespace Tabtaba.Services.Features.SessionServices
{
   public class CancelAppointmentHandler :IRequestHandler<CancelAppointmentCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CancelAppointmentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(CancelAppointmentCommand request,CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Appointment>();
            var appointment = await repo.GetByIdAsync(request.AppointmentId);

            if( appointment == null || appointment.Status == AppointmentStatus.Cancelled )
                return false;


            appointment.Status = AppointmentStatus.Cancelled;


            var patient = await _unitOfWork.GetRepository<Patient>().GetByIdAsync(appointment.PatientId);
            patient!.RemainingSessions += 1;

            _unitOfWork.GetRepository<Patient>().Update(patient);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
    
    
}
