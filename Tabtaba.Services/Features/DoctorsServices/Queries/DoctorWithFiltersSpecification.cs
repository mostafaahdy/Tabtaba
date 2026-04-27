using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Entities;
using Tabtaba.Services.Specifications;

namespace Tabtaba.Services.Features.DoctorsServices.Queries
{
    public class DoctorWithFiltersSpecification :BaseSpecifications<Doctor>
    {
        public DoctorWithFiltersSpecification(string specialization,string diagnosis,decimal? minRate)
            : base(x =>
                (string.IsNullOrEmpty(specialization) || x.Specialization == specialization) &&
            (string.IsNullOrEmpty(diagnosis) || x.Diagnoses.Any(d => d.Diagnosis_Details.Contains(diagnosis))) &&
                (!minRate.HasValue || x.Rating >= minRate.Value)
            )
        {
            AddOrderByDescending(x => x.Rating);
            AddInclude(x => x.Diagnoses);
        }
    }
}