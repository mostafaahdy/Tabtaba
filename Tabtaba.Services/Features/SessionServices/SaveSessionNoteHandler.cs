using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.Session;

namespace Tabtaba.Services.Features.SessionServices;

public class SaveSessionNoteHandler : IRequestHandler<SaveSessionNoteCommand, SaveSessionNoteResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveSessionNoteHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SaveSessionNoteResponse> Handle(
        SaveSessionNoteCommand request,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<SessionNote>();
        var allNotes = await repo.GetAllAsync();
        var existing = allNotes.FirstOrDefault(n => n.AppointmentId == request.AppointmentId);

        if (existing is null)
        {
            await repo.AddAsync(new SessionNote
            {
                AppointmentId = request.AppointmentId,
                Notes = request.Notes,
                Date_Created = DateTime.UtcNow,
                Last_Updated = DateTime.UtcNow
            });
        }
        else
        {
            existing.Notes = request.Notes;
            existing.Last_Updated = DateTime.UtcNow;
            repo.Update(existing);
        }

        await _unitOfWork.SaveChangesAsync();

        return new SaveSessionNoteResponse
        {
            Success = true,
            Message = "Notes saved successfully.",
            LastSaved = DateTime.UtcNow
        };
    }
}