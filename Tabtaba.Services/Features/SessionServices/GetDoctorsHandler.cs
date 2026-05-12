using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Services.Features.DoctorsServices;
using Tabtaba.Services.Specifications;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.Session;

namespace Tabtaba.Services.Features.SessionServices
{
        public class GetDoctorsHandler :IRequestHandler<GetDoctorsQuery,List<GetDoctorDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetDoctorsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

            public async Task<List<GetDoctorDTO>> Handle(GetDoctorsQuery request,CancellationToken cancellationToken)
            {
                var spec = new DoctorWithFiltersSpecification(
                    request.Specialization,
                    request.Diagnosis,
                    request.MinRate);

                var doctors = await _unitOfWork.GetRepository<Doctor>().ListAsync(spec);

                return doctors.Select(d => new GetDoctorDTO
                {
                    Id = d.Id,
                    Name = d.User.FullName, 
                    Specialization = d.Specialization,
                    Rating = (double) d.Rating,
                    DiagnosisNames = d.Diagnoses.Select(diag => diag.Diagnosis_Details).ToList()
                }).ToList();
            }
        }
}

