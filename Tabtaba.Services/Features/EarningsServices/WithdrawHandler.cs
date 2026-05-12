using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.Services.Features.EarningsServices;

public class WithdrawHandler : IRequestHandler<WithdrawCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public WithdrawHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(
        WithdrawCommand request,
        CancellationToken cancellationToken)
    {
        
        var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
        var appointments = await appointmentRepo.GetAllAsync();

        var availableBalance = appointments
            .Where(a => a.DoctorId == request.DoctorId
                     && a.IsPaid
                     && a.Status.ToString() == "Completed")
            .Sum(a => a.Price);

        
        var withdrawalRepo = _unitOfWork.GetRepository<Withdrawal>();
        var withdrawals = await withdrawalRepo.GetAllAsync();

        var totalWithdrawn = withdrawals
            .Where(w => w.DoctorId == request.DoctorId
                     && w.Status != "Rejected")
            .Sum(w => w.Amount);

        var netBalance = availableBalance - totalWithdrawn;

        if (request.Amount > netBalance)
            throw new InvalidOperationException(
                $"Insufficient balance. Available: {netBalance} EGP.");

       
        var withdrawal = new Withdrawal
        {
            DoctorId = request.DoctorId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            Status = "Processing",
            CreatedAt = DateTime.UtcNow
        };

        await withdrawalRepo.AddAsync(withdrawal);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}