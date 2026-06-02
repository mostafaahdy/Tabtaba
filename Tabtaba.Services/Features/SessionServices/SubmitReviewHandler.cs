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
    public class SubmitReviewHandler :IRequestHandler<SubmitReviewCommand,ReviewResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubmitReviewHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReviewResponse> Handle(SubmitReviewCommand request,CancellationToken cancellationToken)
        {
            var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
            var reviewRepo = _unitOfWork.GetRepository<Review>();
            var therapistRepo = _unitOfWork.GetRepository<Therapist>();

            
            var appointment = await appointmentRepo.GetByIdAsync(request.AppointmentId);
            if( appointment == null ) throw new Exception("Appointment not found");
            var therapist = await therapistRepo.GetByIdAsync(appointment.DoctorId);
            string docName = therapist?.User?.FullName ?? "Specialist";

            var newReview = new Review
            {
                AppointmentId = request.AppointmentId,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Rating_Score = request.RatingScore,   
                Review_Text = request.ReviewText,     
                Patient_Notes = request.PatientComment ?? string.Empty,  
                CreatedAt = DateTime.UtcNow
            };

            appointment.Status = AppointmentStatus.Completed;

            await reviewRepo.AddAsync(newReview);
            await _unitOfWork.SaveChangesAsync();

            return new ReviewResponse
            {
                DoctorName = $"Dr. {docName}"
            };
    }   }
}
