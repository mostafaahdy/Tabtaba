using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction;
using Tabtaba.Shared.Chatbot;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBotController :ControllerBase
    {
        private readonly IChatbotService _chatService;

        public ChatBotController(IChatbotService chatService)
        {
            _chatService = chatService;
        }
        [Authorize]
        [HttpPost("Tabtaba Ai Assistent")]
        public async Task<IActionResult> AskBot([FromBody] ChatRequestDTO request)
        {
            // --------------------------- FluentValidation----------------------------
            if( !ModelState.IsValid )
            {
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if( string.IsNullOrEmpty(userIdClaim) )
                {
                    return Unauthorized(new { message = "An error occurred while verifying your identity Please log in again" });
                }

                int patientId = int.Parse(userIdClaim);

                // ---------- AI + Database + Real-time Hub--------------
                var botReply = await _chatService.ProcessUserMessageAsync(patientId,request);

                
                return Ok(new
                {
                    success = true,
                    reply = botReply,
                    sentAt = DateTime.UtcNow
                });
            }
            catch
            {
                return StatusCode(500,new
                {
                    success = false,
                    message = "Something went wrong Please try again later",
                });
            }
        }
    }
}

