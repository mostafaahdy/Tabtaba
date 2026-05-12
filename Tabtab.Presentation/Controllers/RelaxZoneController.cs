using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelaxZoneController :ControllerBase
    {
        private readonly IMediator _mediator;
        public RelaxZoneController(IMediator mediator) => _mediator = mediator;

        
        // Retrieves a list of relaxation content Breathing, Meditation, Music filtered by category
        
        [HttpGet("content")]
        public async Task<IActionResult> GetRelaxContent([FromQuery] string category)
        {
            var result = await _mediator.Send(new GetRelaxContentQuery(category));
            return Ok(result);
        }

       
        [HttpPost("log-activity")]
        public async Task<IActionResult> LogActivity([FromBody] LogRelaxActivityCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if( string.IsNullOrEmpty(userId) ) return Unauthorized();

            command.PatientId = userId;
            var result = await _mediator.Send(command);

            return result ? Ok("Activity logged successfully") : BadRequest("Failed to log activity");
        }
    }
    
}
