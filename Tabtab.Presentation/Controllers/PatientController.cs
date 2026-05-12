using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator) => _mediator = mediator;

        
        [HttpGet("Patient-dashboard")]
        public async Task<IActionResult> GetUserHome()
        {
            var result = await _mediator.Send(new GetUserHomeQuery());
            return Ok(result);
        }


        [HttpPost("add-mood")]
        public async Task<IActionResult> AddMood([FromBody] AddMoodLogCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok("Mood recorded successfully!") : BadRequest("Failed to save");
        }


        [HttpGet("calendar-month-activities")]
        public async Task<IActionResult> GetMonthActivities([FromQuery] int year,[FromQuery] int month)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if( string.IsNullOrEmpty(userId) )
            {
                return Unauthorized(new { message = "User identity not found" });
            }

            var query = new GetCalendarQuery(userId,month,year);
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpGet("mood-stats")]
        public async Task<IActionResult> GetMoodStats()
        {
            var result = await _mediator.Send(new GetMoodAnalyticsQuery());
            return Ok(result);
        }


        [HttpGet("profile-details")]
        public async Task<IActionResult> GetProfileDetails()
        {
            var result = await _mediator.Send(new GetUserProfileQuery());
            return Ok(result);
        }


        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileCommand command)
        {
            var result = await _mediator.Send(command);
            if( result )
            {
                return Ok(new { Message = "Profile updated successfully!" });
            }
            return BadRequest("Failed to update profile.");
        }


        [HttpGet("favorites")]
        public async Task<IActionResult> GetMyFavorites()
        {
            var result = await _mediator.Send(new GetSavedContentQuery());
            return Ok(result);
        }



    }
}
