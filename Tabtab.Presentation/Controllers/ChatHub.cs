using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Tabtaba.Presentation.Controllers;

[Authorize]
public class ChatHub : Hub
{
    public async Task SendMessage(string receiverId, string message)
    {
        await Clients.User(receiverId).SendAsync("ReceiveMessage", new
        {
            senderId = Context.UserIdentifier,
            message,
            sentAt = DateTime.UtcNow
        });
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}