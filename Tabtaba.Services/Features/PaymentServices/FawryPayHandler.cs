using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.BaymentgatewayEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.Payment;

namespace Tabtaba.Services.Features.PaymentServices;

public class FawryPayHandler : IRequestHandler<FawryPayCommand, FawryPayResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private const decimal ServiceFee = 50m;

    public FawryPayHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<FawryPayResponse> Handle(
        FawryPayCommand request,
        CancellationToken cancellationToken)
    {
        // 1️⃣ جيب الـ Appointment
        var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
        var appointments = await appointmentRepo.GetAllAsync();
        var appointment = appointments.FirstOrDefault(a => a.Id == request.AppointmentId);

        if (appointment is null)
            throw new Exception("Appointment not found.");

        if (appointment.IsPaid)
            throw new InvalidOperationException("Appointment already paid.");

        // 2️⃣ جنريت Reference Code
        var referenceCode = GenerateReferenceCode();
        var totalAmount = appointment.Price + ServiceFee;

        // 3️⃣ اعمل Payment
        var payment = new Payment
        {
            PatientId = request.PatientId,
            AppointmentId = request.AppointmentId,
            PaymentMethod = "Fawry",
            WalletPhoneNumber = string.Empty,
            Amount = appointment.Price,
            ServiceFee = ServiceFee,
            TotalAmount = totalAmount,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        var paymentRepo = _unitOfWork.GetRepository<Payment>();
        await paymentRepo.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();

        return new FawryPayResponse
        {
            PaymentId = payment.Id,
            ReferenceCode = referenceCode,
            TotalAmount = totalAmount,
            Status = payment.Status,
            ExpiresAt = DateTime.UtcNow.AddHours(48),
            CreatedAt = payment.CreatedAt
        };
    }

    private static string GenerateReferenceCode()
    {
        var random = new Random();
        return random.Next(1000000000, int.MaxValue).ToString();
    }
}