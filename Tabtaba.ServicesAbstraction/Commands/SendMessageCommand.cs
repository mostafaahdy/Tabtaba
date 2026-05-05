using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Chat;

namespace Tabtaba.ServicesAbstraction.Commands;

public record SendMessageCommand(
    string SenderId,
    string ReceiverId,
    string Content
) : IRequest<MessageDto>;