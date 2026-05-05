using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Contracts;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Chat;

namespace Tabtaba.Services.Features.ChatServices;

public class SendMessageHandler : IRequestHandler<SendMessageCommand, MessageDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public SendMessageHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<MessageDto> Handle(
        SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        var message = new Message
        {
            SenderId = request.SenderId,
            ReceiverId = request.ReceiverId,
            Content = request.Content,
            SentAt = DateTime.UtcNow,
            IsRead = false
        };

        await _unitOfWork.GetRepository<Message>().AddAsync(message);
        await _unitOfWork.SaveChangesAsync();

        var sender = await _userManager.FindByIdAsync(request.SenderId);

        return new MessageDto
        {
            Id = message.Id,
            Content = message.Content,
            SenderId = message.SenderId,
            SenderName = sender?.FullName + " " + sender?.L_Name,
            SentAt = message.SentAt,
            IsRead = message.IsRead
        };
    }
}