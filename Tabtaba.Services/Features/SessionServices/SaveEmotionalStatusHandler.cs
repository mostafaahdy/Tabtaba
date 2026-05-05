using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Commands;

namespace Tabtaba.Services.Features.SessionServices;

public class SaveEmotionalStatusHandler : IRequestHandler<SaveEmotionalStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveEmotionalStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        SaveEmotionalStatusCommand request,
        CancellationToken cancellationToken)
    {
        var appointments = await _unitOfWork.GetRepository<Appointment>().GetAllAsync();
        var appointment = appointments.FirstOrDefault(a => a.Id == request.AppointmentId);

        if (appointment is null)
            return false;

        await _unitOfWork.GetRepository<ProgressTracker>().AddAsync(new ProgressTracker
        {
            PatientId = appointment.PatientId,
            Mood_Score = request.MoodScore,
            Date_Recorded = DateTime.UtcNow,
            Anxiety_Level = 0,
            Sleep_Hours = 0
        });

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}