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
    public class CreateCalendarBookingHandler :IRequestHandler<CreateCalendarBookingCommand,CalendarBookingResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCalendarBookingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CalendarBookingResponse> Handle(CreateCalendarBookingCommand request,CancellationToken cancellationToken)
        {
            var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
            var therapistRepo = _unitOfWork.GetRepository<Therapist>();
            var patientRepo = _unitOfWork.GetRepository<Patient>(); 

            var therapist = await therapistRepo.GetByIdAsync(request.DoctorId);
            string docName = therapist?.FullName ?? "Specialist";
            string speciality = therapist?.Specialization ?? "Mental Health Consultant";

            DateTime finalDateTime = request.SelectedDate.Date.Add(DateTime.Parse(request.SelectedTime).TimeOfDay);

           
            var patient = await patientRepo.GetByIdAsync(request.PatientId);
            string displayPrice = "$20"; 

            if( patient != null && patient.RemainingSessions > 0 )
            {
                displayPrice = "$0 (Plan Included)";
                patient.RemainingSessions -= 1; 
                                                
                patientRepo.Update(patient);
            }

            string actualSessionType = string.IsNullOrEmpty(request.SessionType) ? "Video" : request.SessionType;
            var newAppointment = new Appointment
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                Date_Time = finalDateTime,
                Duration_Minutes = request.Duration_Minutes,
                Session_Type = actualSessionType,
                Status = AppointmentStatus.Confirmed,
                Zoom_Meeting_Url = "https://zoom.us/j/" + Guid.NewGuid().ToString().Substring(0,8)
            };

            await appointmentRepo.AddAsync(newAppointment);
            await _unitOfWork.SaveChangesAsync();
            return new CalendarBookingResponse
            {
                DoctorName = $"Dr. {docName}",
                Speciality = speciality,
                AppointmentDate = finalDateTime.ToString("dddd, MMM dd"), // Monday, Oct 24
                AppointmentTime = finalDateTime.ToString("hh:mm tt"),    // 02:30 PM
                Duration = $"{request.Duration_Minutes} min",
                SessionType = actualSessionType == "Video" ? "Virtual Sanctuary" : actualSessionType,
                Price = displayPrice,
                ConfirmationText = $"You booked an appointment with Dr. {docName} on {finalDateTime:MMMM dd}, at {finalDateTime:hh:mm tt}"
            };
        }
    }
}

