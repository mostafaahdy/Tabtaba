using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.Services.Features.PaymentServices;

public class VodafonePayHandler : IRequestHandler<VodafonePayCommand, VodafonePayResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private const decimal ServiceFee = 50m; 

    public VodafonePayHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<VodafonePayResponse> Handle(
        VodafonePayCommand request,
        CancellationToken cancellationToken)
    {
        
        var patientRepo = _unitOfWork.GetRepository<Patient>();
        var patient = await patientRepo.GetByIdAsync(request.PatientId);
        if (patient is null)
            throw new KeyNotFoundException("Patient not found.");

        
        var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
        var appointment = await appointmentRepo.GetByIdAsync(request.AppointmentId);
        if (appointment is null)
            throw new KeyNotFoundException("Appointment not found.");

        if (appointment.IsPaid)
            throw new InvalidOperationException("Appointment already paid.");

        var totalAmount = appointment.Price + ServiceFee;

        var payment = new Payment
        {
            PatientId = request.PatientId,
            AppointmentId = request.AppointmentId,
            PaymentMethod = "VodafoneCash",
            WalletPhoneNumber = request.WalletPhoneNumber,
            Amount = appointment.Price,
            ServiceFee = ServiceFee,
            TotalAmount = totalAmount,
            Status = "Paid", 
            CreatedAt = DateTime.UtcNow
        };

        var paymentRepo = _unitOfWork.GetRepository<Payment>();
        await paymentRepo.AddAsync(payment);

        appointment.IsPaid = true;
        appointment.Status = "Confirmed";
        await _unitOfWork.SaveChangesAsync();

        return new VodafonePayResponse
        {
            PaymentId = payment.Id,
            Status = payment.Status,
            AmountDue = payment.Amount,
            ServiceFee = payment.ServiceFee,
            TotalAmount = payment.TotalAmount,
            WalletPhoneNumber = payment.WalletPhoneNumber,
            CreatedAt = payment.CreatedAt
        };
    }
}