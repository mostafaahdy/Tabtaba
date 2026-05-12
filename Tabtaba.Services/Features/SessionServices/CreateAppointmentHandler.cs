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
    public class CreateAppointmentHandler :IRequestHandler<CreateAppointmentCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> Handle(CreateAppointmentCommand request,CancellationToken cancellationToken)
        {
            var patientRepo = _unitOfWork.GetRepository<Patient>();
            var appointmentRepo = _unitOfWork.GetRepository<Appointment>();

            var patient = await patientRepo.GetByIdAsync(request.PatientId);
            if( patient == null )
                throw new KeyNotFoundException("Patient not found");

            if( patient.RemainingSessions <= 0 )
            {
                throw new InvalidOperationException("NoSessionsLeft");
            }

            var allAppointments = await appointmentRepo.GetAllAsync();

            var hasActiveSessionsWithOtherDoctor = allAppointments.Any(a =>
                a.PatientId == request.PatientId &&
                a.DoctorId != request.DoctorId &&
                a.Status != AppointmentStatus.Completed &&
                a.Status != AppointmentStatus.Cancelled);

            if( hasActiveSessionsWithOtherDoctor )
            {
                throw new InvalidOperationException("Different Doctor Binding");
            }

            var appointment = new Appointment
            {
                Date_Time = request.Date_Time,
                Session_Type = request.Session_Type,
                Duration_Minutes = request.Duration_Minutes,
                Location_Mode = request.Location_Mode,
                Price = request.Price,
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                Status = AppointmentStatus.Pending,
                IsPaid = true 
            };

            await appointmentRepo.AddAsync(appointment);

            patient.RemainingSessions -= 1;
            patientRepo.Update(patient);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
    
