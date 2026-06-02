using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Schedule;

namespace Tabtaba.Services.Features.DoctorsServices;

public class GetWeeklyScheduleHandler : IRequestHandler<GetWeeklyScheduleQuery, WeeklyScheduleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetWeeklyScheduleHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<WeeklyScheduleResponse> Handle(
        GetWeeklyScheduleQuery request,
        CancellationToken cancellationToken)
    {
        // ── Get Doctor ─────────────────────────────────────────────
        var doctors = await _unitOfWork.GetRepository<Doctor>().GetAllAsync();
        var doctor = doctors.FirstOrDefault(d => d.UserId == request.DoctorId);

        if (doctor is null)
            throw new UnauthorizedAccessException("Doctor not found.");

        // ── Get Week Range ─────────────────────────────────────────
        var weekStart = request.WeekStartDate.Date;
        var weekEnd = weekStart.AddDays(7);

        // ── Get Appointments ───────────────────────────────────────
        var allAppointments = await _unitOfWork.GetRepository<Appointment>().GetAllAsync();
        var weekAppointments = allAppointments
            .Where(a => a.DoctorId == doctor.Id
                     && a.Date_Time >= weekStart
                     && a.Date_Time < weekEnd)
            .OrderBy(a => a.Date_Time)
            .ToList();

        // ── Map to DTO ─────────────────────────────────────────────
        var sessions = weekAppointments.Select(a => new SessionItemDto
        {
            AppointmentId = a.Id,
            PatientName = a.Patient?.User?.FullName + " " + a.Patient?.User?.LastName,
            SessionTime = a.Date_Time,
            DurationMinutes = a.Duration_Minutes,
            SessionType = a.Session_Type,
            LocationMode = a.Location_Mode,
            Status = a.Status.ToString()
        }).ToList();

        return new WeeklyScheduleResponse
        {
            Sessions = sessions,
            TotalSessions = sessions.Count,
            VideoSessions = sessions.Count(s => s.LocationMode == "Video"),
            ChatSessions = sessions.Count(s => s.LocationMode == "Chat")
        };
    }
}
