using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction;
using Tabtaba.Shared.Chatbot;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatbotService _chatService;

        public ChatBotController(IChatbotService chatService)
        {
            _chatService = chatService;
        }

        [Authorize]
        [HttpPost("ask")] // المسار النضيف السلكان اللي هو عاوزه
        public async Task<IActionResult> AskBot([FromBody] ChatRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { success = false, message = "Identity verification failed." });
                }

                int patientId = int.Parse(userIdClaim);

                var botReply = await _chatService.ProcessUserMessageAsync(patientId, request);

                return Ok(new
                {
                    success = true,
                    reply = botReply,
                    sentAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Something went wrong.",
                    error = ex.Message
                });
            }
        }
    }
}