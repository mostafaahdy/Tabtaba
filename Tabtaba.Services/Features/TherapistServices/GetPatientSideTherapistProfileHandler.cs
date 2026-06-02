using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.Services.Features.TherapistServices
{
    public class GetPatientSideTherapistProfileHandler
     :IRequestHandler<GetPatientSideTherapistProfileQuery,PatientSideTherapistProfileResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientSideTherapistProfileHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<PatientSideTherapistProfileResponse> Handle(GetPatientSideTherapistProfileQuery request,CancellationToken cancellationToken)
        {
            var therapist = await _unitOfWork.GetRepository<Therapist>().GetByIdAsync(request.TherapistId);
            if( therapist == null ) throw new Exception("Therapist not found");

            var allDoctors = await _unitOfWork.GetRepository<Doctor>().GetAllAsync();
            var doctor = allDoctors.FirstOrDefault(d => d.UserId == therapist.UserId.ToString());

            var reviews = doctor?.Reviews?.ToList() ?? new List<Review>();
            double avgRating = reviews.Any() ? (double) reviews.Average(r => r.Rating_Score) : (double) (doctor?.Rating ?? 0);

            int matchScore = (int) ((avgRating * 15) + ((doctor?.Years_Experience ?? 0) * 2));
            matchScore = Math.Clamp(matchScore,80,98);

            return new PatientSideTherapistProfileResponse
            {
                Id = therapist.Id,
                FullName = $"Dr. {therapist.User?.FullName}",
                ProfilePictureUrl = therapist.ProfilePictureUrl,
                Title = doctor?.Specialization ?? therapist.Specialization,   

                Rating = Math.Round(avgRating,1),
                ReviewsCount = reviews.Count,
                MatchPercentage = matchScore,

                IntroAudioUrl = therapist.CvUrl,

                Quote = therapist.Bio,
                Description = therapist.ProfessionalInfo?.Therapist.Bio ?? "Expert in providing mental health support.",

                ExperienceLabel = $"+{doctor?.Years_Experience ?? therapist.YearsOfExperience ?? 0} years",
                EducationLabel = therapist.Educations.FirstOrDefault()?.HighestDegree ?? "Specialist",

                SpecialtyTags = (doctor?.Specialization ?? therapist.Specialization)?
                                .Split(',').Select(s => s.Trim()).ToList() ?? new List<string>(),

                //  Education Section
                Certificates = therapist.Educations.Select(e => new CertificateItemDto
                {
                    Title = e.UniversityName,
                    SubTitle = $"{e.HighestDegree} in {e.HighestDegree}"
                }).ToList(),

                // Horizontal Calendar
                WeeklyAvailableSlots = therapist.Availabilities
                    .Where(a => a.IsAvailable)
                    .OrderBy(a => a.DayOfWeek)
                    .Select(a => new DailySlotsDto
                    {
                        DayName = a.DayOfWeek.ToString().Substring(0,3).ToUpper(), // MON, TUE...
                        DayNumber = GetNextDateForDay(a.DayOfWeek).Day, 
                        Times = new List<string> { a.StartTime.ToString("hh:mm tt") } // 10:00 AM
                    }).ToList(),

                // Top Review
                TopReviews = reviews.OrderByDescending(r => r.Id).Take(1).Select(r => new PatientReviewDto
                {
                    StarRating = (double) r.Rating_Score,
                    Comment = "Highly recommended for professional support"
                }).ToList()
            };
        }

        private DateTime GetNextDateForDay(DayOfWeek day)
        {
            int start = (int) DateTime.Today.DayOfWeek;
            int target = (int) day;
            if( target <= start ) target += 7;
            return DateTime.Today.AddDays(target - start);
        }
    }
}
