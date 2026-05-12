using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Therapist
{
    public class PatientSideTherapistProfileResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? ProfilePictureUrl { get; set; }
        public string? Title { get; set; } // Clinical Psychologist
        public double Rating { get; set; } // 4.8
        public int ReviewsCount { get; set; } // 120
        public int MatchPercentage { get; set; } // 92%
        public string? IntroAudioUrl { get; set; } // Listen to Intro

        public string? Quote { get; set; } // "Helping you understand..."
        public string? Description { get; set; } // الوصف الطويل
        public string? ExperienceLabel { get; set; } // +8 years
        public string? EducationLabel { get; set; } // CBT Certified

        public List<string> SpecialtyTags { get; set; } = []; // Anxiety, OCD...

        public List<CertificateItemDto> Certificates { get; set; } = [];

        //   (Available Slots)
        public List<DailySlotsDto> WeeklyAvailableSlots { get; set; } = [];

        public List<PatientReviewDto> TopReviews { get; set; } = [];
    }
}
