using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Doctor;
namespace Tabtaba.Services.Features.DoctorServices;

public class GetDoctorDashboardHandler : IRequestHandler<GetDoctorDashboardQuery, DoctorDashboardResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetDoctorDashboardHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<DoctorDashboardResponse> Handle(
        GetDoctorDashboardQuery request,
        CancellationToken cancellationToken)
    {
        // ── Get Doctor ─────────────────────────────────────────────
        var user = await _userManager.FindByIdAsync(request.DoctorId);
        if (user is null)
            throw new UnauthorizedAccessException("Doctor not found.");

        var doctors = await _unitOfWork.GetRepository<Doctor>().GetAllAsync();
        var doctor = doctors.FirstOrDefault(d => d.UserId == request.DoctorId);

        if (doctor is null)
            throw new UnauthorizedAccessException("Doctor profile not found.");

        var today = DateTime.UtcNow.Date;
        var lastWeek = today.AddDays(-7);

        // ── All Appointments ───────────────────────────────────────
        var allAppointments = await _unitOfWork.GetRepository<Appointment>().GetAllAsync();
        var doctorAppointments = allAppointments.Where(a => a.DoctorId == doctor.Id).ToList();

        // ── Total Sessions ─────────────────────────────────────────
        var totalSessions = doctorAppointments.Count;

        // ── Today's Sessions ───────────────────────────────────────
        var todaySessions = doctorAppointments
            .Where(a => a.Date_Time.Date == today)
            .Select(a => new TodaySessionDto
            {
                PatientName = a.Patient?.User?.FullName + " " + a.Patient?.User?.LastName,
                SessionType = a.Session_Type,
                SessionTime = a.Date_Time
            })
            .ToList();

        // ── Remaining Sessions Today ───────────────────────────────
        var remainingSessions = todaySessions
            .Count(s => s.SessionTime > DateTime.UtcNow);

        // ── Total Earnings ─────────────────────────────────────────
        var totalEarnings = doctorAppointments
            .Where(a => a.IsPaid)
            .Sum(a => a.Price);

        // ── Earnings Percentage ────────────────────────────────────
        var earningsThisWeek = doctorAppointments
            .Where(a => a.IsPaid && a.Date_Time >= lastWeek)
            .Sum(a => a.Price);

        var earningsPrevWeek = doctorAppointments
            .Where(a => a.IsPaid
                     && a.Date_Time >= lastWeek.AddDays(-7)
                     && a.Date_Time < lastWeek)
            .Sum(a => a.Price);

        var earningsPercentage = earningsPrevWeek == 0 ? 100 :
            Math.Round((double)(earningsThisWeek - earningsPrevWeek)
                / (double)earningsPrevWeek * 100, 1);

        // ── Rating ─────────────────────────────────────────────────
        var reviews = await _unitOfWork.GetRepository<Review>().GetAllAsync();
        var doctorReviews = reviews.Where(r => r.DoctorId == doctor.Id).ToList();
        var rating = doctorReviews.Any()
            ? Math.Round(doctorReviews.Average(r => (double)r.Rating_Score), 1)
            : 0.0;

        return new DoctorDashboardResponse
        {
            DoctorName = user.FullName + " " + user.LastName,
            TotalEarnings = totalEarnings,
            EarningsPercentage = earningsPercentage,
            TotalSessions = totalSessions,
            Rating = rating,
            RemainingSessions = remainingSessions,
            TodaySessionsList = todaySessions
        };
    }
}