using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SettingsController :ControllerBase
    {
        private readonly IMediator _mediator;
        public SettingsController(IMediator mediator) => _mediator = mediator;


        [HttpGet("Get-settings")]
        public async Task<IActionResult> GetSettings() => Ok(await _mediator.Send(new GetUserSettingsQuery()));


        [HttpPut("Update-settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] UpdateSettingsCommand command)
            => Ok(await _mediator.Send(command));


        [HttpPut("privacy-settings")]
        public async Task<IActionResult> UpdatePrivacy([FromBody] UpdatePrivacySettingsCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok(new { Message = "Privacy settings updated!" }) : BadRequest();
        }


        [HttpGet("notifications-settings")]
        public async Task<IActionResult> GetNotificationSettings()
        {
            var result = await _mediator.Send(new GetNotificationSettingsQuery());
            return Ok(result);
        }


        [HttpPut("update-notifications")]
        public async Task<IActionResult> UpdateNotificationSettings([FromBody] UpdateNotificationPreferencesCommand command)
        {
            var result = await _mediator.Send(command);
            if( result )
            {
                return Ok(new { Message = "Notification settings updated successfully!" });
            }
            return BadRequest("Failed to update settings.");
        }

    }
}
