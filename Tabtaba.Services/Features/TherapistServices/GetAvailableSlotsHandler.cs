using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.Services.Features.TherapistServices
{
    public class GetAvailableSlotsHandler :IRequestHandler<GetAvailableSlotsQuery,AvailableSlotsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAvailableSlotsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AvailableSlotsResponse> Handle(GetAvailableSlotsQuery request,CancellationToken cancellationToken)
        {
            if( request.SelectedDate.Date < DateTime.Now.Date )
            {
                return new AvailableSlotsResponse { SelectedDate = request.SelectedDate };
            }

            var allAvailabilities = await _unitOfWork.GetRepository<TherapistAvailability>().GetAllAsync();
            var availability = allAvailabilities.FirstOrDefault(a =>
                a.TherapistId == request.TherapistGuid &&
                a.DayOfWeek == request.SelectedDate.DayOfWeek &&
                a.IsAvailable);

            if( availability == null )
                return new AvailableSlotsResponse { SelectedDate = request.SelectedDate };

            var allAppointments = await _unitOfWork.GetRepository<Appointment>().GetAllAsync();
            var bookedSlots = allAppointments
                .Where(a => a.DoctorId == request.DoctorIntId && a.Date_Time.Date == request.SelectedDate.Date)
                .Select(a => a.Date_Time.TimeOfDay)
                .ToList();

            var response = new AvailableSlotsResponse { SelectedDate = request.SelectedDate };
            var afternoonGroup = new SlotGroupDTO { GroupName = "Afternoon" };
            var eveningGroup = new SlotGroupDTO { GroupName = "Evening" };

            var currentTime = availability.StartTime;
            int sessionDurationMinutes = 60;

            while( currentTime < availability.EndTime )
            {
                var slotDateTime = request.SelectedDate.Date.Add(currentTime.ToTimeSpan());

                bool isBooked = bookedSlots.Any(b => b.Hours == currentTime.Hour && b.Minutes == currentTime.Minute);
                bool isPast = slotDateTime <= DateTime.Now;

                var slot = new SlotDetailDTO
                {
                    Time = slotDateTime.ToString("h:mm tt"),
                    IsAvailable = !isBooked && !isPast,
                    IsSelected = false 
                };

                if( currentTime.Hour < 16 )
                    afternoonGroup.Slots.Add(slot);
                else
                    eveningGroup.Slots.Add(slot);

                currentTime = currentTime.AddMinutes(sessionDurationMinutes);
            }

            response.SlotGroups.Add(afternoonGroup);
            response.SlotGroups.Add(eveningGroup);

            return response;
        }
    }
}
