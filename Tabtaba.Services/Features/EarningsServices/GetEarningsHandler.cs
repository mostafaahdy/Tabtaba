using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Earnings;

namespace Tabtaba.Services.Features.EarningsServices;

public class GetEarningsHandler : IRequestHandler<GetEarningsQuery, EarningsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetEarningsHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<EarningsResponse> Handle(
        GetEarningsQuery request,
        CancellationToken cancellationToken)
    {
        
        var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
        var allAppointments = await appointmentRepo.GetAllAsync();
        var appointments = allAppointments
            .Where(a => a.DoctorId == request.DoctorId)
            .ToList();

       
        var withdrawalRepo = _unitOfWork.GetRepository<Withdrawal>();
        var allWithdrawals = await withdrawalRepo.GetAllAsync();
        var withdrawals = allWithdrawals
            .Where(w => w.DoctorId == request.DoctorId
                     && w.Status != "Rejected")
            .ToList();

        var totalWithdrawn = withdrawals.Sum(w => w.Amount);

        
        var total = appointments
            .Where(a => a.IsPaid && a.Status.ToString() == "Completed")
            .Sum(a => a.Price) - totalWithdrawn;

        var pending = appointments
            .Where(a => !a.IsPaid)
            .Sum(a => a.Price);

       
        var thisMonth = DateTime.UtcNow.Month;
        var lastMonth = thisMonth == 1 ? 12 : thisMonth - 1;

        var thisMonthTotal = appointments
            .Where(a => a.IsPaid && a.Date_Time.Month == thisMonth)
            .Sum(a => a.Price);

        var lastMonthTotal = appointments
            .Where(a => a.IsPaid && a.Date_Time.Month == lastMonth)
            .Sum(a => a.Price);

        var growth = lastMonthTotal == 0 ? 0
            : Math.Round((thisMonthTotal - lastMonthTotal) / lastMonthTotal * 100, 1);

        
        var earningsAnalysis = Enumerable.Range(0, 6)
            .Select(i =>
            {
                var date = DateTime.UtcNow.AddMonths(-i);
                var amount = appointments
                    .Where(a => a.IsPaid
                        && a.Date_Time.Month == date.Month
                        && a.Date_Time.Year == date.Year)
                    .Sum(a => a.Price);
                return new MonthlyEarning
                {
                    Month = date.ToString("MMM"),
                    Amount = amount
                };
            })
            .Reverse()
            .ToList();

        // 6️⃣ Recent Transactions
        var recentTransactions = appointments
            .OrderByDescending(a => a.Date_Time)
            .Take(10)
            .Select(a => new TransactionDto
            {
                Description = "Consultation Session",
                Amount = a.IsPaid ? a.Price : -a.Price,
                Status = a.Status.ToString(),
                Date = a.Date_Time
            })
            .ToList();

        return new EarningsResponse
        {
            TotalAvailableEarnings = total,
            PendingAmount = pending,
            MonthlyGrowthPercentage = growth,
            EarningsAnalysis = earningsAnalysis,
            RecentTransactions = recentTransactions
        };
    }
}