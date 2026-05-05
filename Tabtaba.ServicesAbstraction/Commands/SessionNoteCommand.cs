using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Session;

namespace Tabtaba.ServicesAbstraction.Commands;

public record SaveSessionNoteCommand(
    int AppointmentId,
    string Notes
) : IRequest<SaveSessionNoteResponse>;