using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Contracts;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.DTOs.Chat;

namespace Tabtaba.Services.Features.ChatServices;

public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetMessagesHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<List<MessageDto>> Handle(
        GetMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await _unitOfWork.GetRepository<Message>().GetAllAsync();

        var conversation = messages
            .Where(m => (m.SenderId == request.UserId && m.ReceiverId == request.OtherUserId) ||
                       (m.SenderId == request.OtherUserId && m.ReceiverId == request.UserId))
            .OrderBy(m => m.SentAt)
            .ToList();

        var result = new List<MessageDto>();

        foreach (var msg in conversation)
        {
            var sender = await _userManager.FindByIdAsync(msg.SenderId);
            result.Add(new MessageDto
            {
                Id = msg.Id,
                Content = msg.Content,
                SenderId = msg.SenderId,
                SenderName = sender?.FullName + " " + sender?.L_Name,
                SentAt = msg.SentAt,
                IsRead = msg.IsRead
            });
        }

        return result;
    }
}