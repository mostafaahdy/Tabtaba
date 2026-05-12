using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Chat;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
        => _mediator = mediator;

    // ── Get Conversations ──────────────────────────────────────────────────
    [HttpGet("conversations")]
    public async Task<IActionResult> GetConversations()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var query = new GetConversationsQuery(userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // ── Get Messages ───────────────────────────────────────────────────────
    [HttpGet("messages/{otherUserId}")]
    public async Task<IActionResult> GetMessages(string otherUserId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var query = new GetMessagesQuery(userId, otherUserId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // ── Send Message ───────────────────────────────────────────────────────
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var command = new SendMessageCommand(userId, request.ReceiverId, request.Content);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}