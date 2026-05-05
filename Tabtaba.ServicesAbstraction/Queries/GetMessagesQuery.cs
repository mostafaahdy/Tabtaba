using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Tabtaba.Shared.DTOs.Chat;

namespace Tabtaba.ServicesAbstraction.Queries;

public record GetMessagesQuery(
    string UserId,
    string OtherUserId
) : IRequest<List<MessageDto>>;