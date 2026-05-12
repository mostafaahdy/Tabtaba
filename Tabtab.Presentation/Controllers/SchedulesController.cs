using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SchedulesController :ControllerBase
    {
        private readonly IMediator _mediator;

        public SchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available-slots")]
        public async Task<IActionResult> GetAvailableSlots(
            [FromQuery] Guid therapistGuid,
            [FromQuery] int doctorIntId,
            [FromQuery] DateTime date)
        {
            if( therapistGuid == Guid.Empty || doctorIntId <= 0 )
            {
                return BadRequest("The Doctor Data Not Valid");
            }

            var query = new GetAvailableSlotsQuery(therapistGuid,doctorIntId,date);
            var result = await _mediator.Send(query);

            if( result == null )
            {
                return NotFound("No available slots for this doctor on the selected day");
            }

            return Ok(result);
        }
    }
}
