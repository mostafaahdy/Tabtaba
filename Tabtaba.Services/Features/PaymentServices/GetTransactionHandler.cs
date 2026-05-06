using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.Services.Features.PaymentServices;

public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, TransactionResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransactionHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<TransactionResponse> Handle(
        GetTransactionQuery request,
        CancellationToken cancellationToken)
    {
        //  Payment
        var paymentRepo = _unitOfWork.GetRepository<Payment>();
        var payments = await paymentRepo.GetAllAsync();
        var payment = payments.FirstOrDefault(p => p.Id == request.PaymentId);

        if (payment is null)
            throw new KeyNotFoundException("Payment not found.");

        //  Appointment
        var appointmentRepo = _unitOfWork.GetRepository<Appointment>();
        var appointments = await appointmentRepo.GetAllAsync();
        var appointment = appointments.FirstOrDefault(a => a.Id == payment.AppointmentId);

        if (appointment is null)
            throw new KeyNotFoundException("Appointment not found.");

        //  Doctor
        var doctorRepo = _unitOfWork.GetRepository<Doctor>();
        var doctors = await doctorRepo.GetAllAsync();
        var doctor = doctors.FirstOrDefault(d => d.Id == appointment.DoctorId);

        return new TransactionResponse
        {
            PaymentId = payment.Id,
            TransactionId = $"TXN-{payment.Id:D5}-{payment.PatientId}",
            AmountPaid = payment.TotalAmount,
            Status = payment.Status,
            Date = payment.CreatedAt,
            Time = payment.CreatedAt.ToString("hh:mm tt"),
            AppointmentSummary = new AppointmentSummaryDto
            {
                DoctorName = doctor?.User?.FullName ?? "Unknown",
                Specialization = doctor?.Specialization ?? "Unknown",
                AppointmentDate = appointment.Date_Time
            }
        };
    }
}