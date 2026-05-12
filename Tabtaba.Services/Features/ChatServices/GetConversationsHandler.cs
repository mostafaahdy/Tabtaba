using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Chat;

namespace Tabtaba.Services.Features.ChatServices;

public class GetConversationsHandler : IRequestHandler<GetConversationsQuery, List<ConversationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public GetConversationsHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<List<ConversationDto>> Handle(
        GetConversationsQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await _unitOfWork.GetRepository<Message>().GetAllAsync();

        var conversations = messages
            .Where(m => m.SenderId == request.UserId || m.ReceiverId == request.UserId)
            .GroupBy(m => m.SenderId == request.UserId ? m.ReceiverId : m.SenderId)
            .Select(g => new
            {
                OtherUserId = g.Key,
                LastMessage = g.OrderByDescending(m => m.SentAt).First()
            })
            .ToList();

        var result = new List<ConversationDto>();

        foreach (var conv in conversations)
        {
            var otherUser = await _userManager.FindByIdAsync(conv.OtherUserId);
            if (otherUser is null) continue;

            var unreadCount = messages.Count(m =>
                m.SenderId == conv.OtherUserId &&
                m.ReceiverId == request.UserId &&
                !m.IsRead);

            result.Add(new ConversationDto
            {
                PatientId = conv.OtherUserId,
                PatientName = otherUser.FullName + " " + otherUser.L_Name,
                LastMessage = conv.LastMessage.Content,
                LastMessageTime = conv.LastMessage.SentAt,
                UnreadCount = unreadCount,
                IsOnline = false
            });
        }

        return result.OrderByDescending(c => c.LastMessageTime).ToList();
    }
}