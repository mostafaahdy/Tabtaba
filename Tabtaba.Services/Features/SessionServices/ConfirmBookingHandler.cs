using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.Shared.Session;

namespace Tabtaba.Services.Features.SessionServices
{
    public class ConfirmBookingHandler :IRequestHandler<ConfirmBookingCommand,BookingResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConfirmBookingHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<BookingResponseDTO> Handle(ConfirmBookingCommand request,CancellationToken cancellationToken)
        {
            var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
            var therapistRepo = _unitOfWork.GetRepository<Therapist>();
             var therapist = (await therapistRepo.GetAllAsync())
                .FirstOrDefault(t => t.UserId == request.TherapistId);

            if( therapist == null ) throw new Exception("Therapist not found!");

            DateTime appointmentDay = request.SelectedDate.Date;
            TimeSpan appointmentTime = DateTime.Parse(request.SelectedTime).TimeOfDay;
            DateTime finalDateTime = appointmentDay.Add(appointmentTime);

            var newAppointment = new Appointment
            {
                DoctorId = request.DoctorIntId,
                PatientId = request.PatientId,
                Date_Time = finalDateTime,
                Session_Type = request.SessionType, // "Video Call"
                Duration_Minutes = request.Duration,
                Price = request.Price,                 
                Status = AppointmentStatus.Pending,      
                Zoom_Meeting_Url = request.ZoomUrl,
                Zoom_Meeting_Id = request.ZoomId
            };

            await appointmentRepo.AddAsync(newAppointment);
            await _unitOfWork.SaveChangesAsync();
            return new BookingResponseDTO
            {
                Message = "Session Booked!",
                DoctorName = therapist?.FullName ?? "Unknown Doctor",
                FullDateTime = finalDateTime.ToString("MMM dd, yyyy - hh:mm tt"),
                SessionType = request.SessionType,
                Amount = request.Price
            };

        }


    }
}
