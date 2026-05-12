using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.Therapist;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SessionsController :ControllerBase
    {
        private readonly IMediator _mediator;
        public SessionsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("Get-Sessions")]
        public async Task<IActionResult> GetSessionTrack()
        {

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if( string.IsNullOrEmpty(userIdClaim) )
                return Unauthorized("You Must Login First");
            var query = new GetSessionTrackQuery(int.Parse(userIdClaim));
            var result = await _mediator.Send(query);

            if( result == null )
                return NotFound("There is no data available for this Sessions ");

            return Ok(result);
        }


        [HttpGet("profile-Therapist/{id:guid}")]
        [ProducesResponseType(typeof(PatientSideTherapistProfileResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PatientSideTherapistProfileResponse>> GetDetailedProfile(Guid id)
        {
            try
            {

                var result = await _mediator.Send(new GetPatientSideTherapistProfileQuery(id));

                if( result == null )
                {
                    return NotFound(new
                    {
                        Message = "No processor data was found  please ensure the correct ID is entered"
                    });
                }

                return Ok(result);
            }
            catch( Exception ex )
            {
                return StatusCode(500,new
                {
                    Error = "Internal Server Error",
                    Details = ex.Message
                });
            }
        }

        [HttpPost("calendar-booking")]
        public async Task<IActionResult> CreateCalendarBooking([FromBody] CreateCalendarBookingCommand command)
        {
            if( command == null )
            {
                return BadRequest(new { Message = "Data is missing" });
            }

            var response = await _mediator.Send(command);

            // (Title, SubTitle, DoctorName, AppointmentDate, AppointmentTime, ConfirmationText)
            return Ok(response);
        }


        [HttpPost("confirm-booking")]
        public async Task<IActionResult> ConfirmBooking([FromBody] ConfirmBookingCommand command)
        {
            if( command == null )
            {
                return BadRequest(new { Message = "Data is missing" });
            }
            var result = await _mediator.Send(command);

            if( result != null )
            {
                return Ok(result);
            }

            return BadRequest(new { message = "Sorry, there was an error while confirming the booking" });
        }


        [HttpPost("Create-Appointment")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if( result )
                    return Ok(new { Message = "The Appointment has been successfully created." });

                return BadRequest("An error occurred while creating the appointment.");
            }
            catch( InvalidOperationException ex )
            {
                if( ex.Message == "NoSessionsLeft" )
                    return BadRequest(new { ErrorCode = "NO_SESSIONS",Message = "Sorry, you do not have enough session credits. Please recharge a new package." });

                if( ex.Message == "DifferentDoctorBinding" )
                    return BadRequest(new { ErrorCode = "DOCTOR_MISMATCH",Message = "You cannot book with another doctor until your current sessions are completed." });
                return BadRequest(ex.Message);
            }
            catch( Exception ex )
            {
                return StatusCode(500,"Internal server error: " + ex.Message);
            }
        }

        [HttpPut("cancel-appointment")]
        public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentCommand command)
        {
            if( command == null )
            {
                return BadRequest(new { Message = "Appointment data is required" });
            }

            var result = await _mediator.Send(command);

            if( result )
            {
                return Ok(new { Message = "The appointment has been successfully cancelled and your session credit has been restored" });
            }

            return BadRequest(new { Message = "Sorry, we couldn't cancel this appointment It might be already cancelled or not found" });
        }


        [HttpGet("upcoming-session-reminder/{patientId}")]
        public async Task<IActionResult> GetUpcomingSessionReminder(int patientId)
        {
            var query = new GetSessionReminderQuery(patientId);
            var response = await _mediator.Send(query);

            if( response == null )
            {
                return NotFound(new { Message = "No upcoming sessions found for this patient" });
            }
            return Ok(response);
        }


        [HttpPost("submit-feedback")]
        public async Task<IActionResult> SubmitFeedback([FromBody] SubmitReviewCommand command)
        {
            if( command == null )
            {
                return BadRequest(new { Message = "Review data is required" });
            }

            try
            {
                var result = await _mediator.Send(command);

                if( result.IsSuccess )
                {
                    return Ok(result);
                }

                return BadRequest(new { Message = "Could not submit feedback" });
            }
            catch( Exception ex )
            {
                return StatusCode(500,new { Message = "Internal Server Error",Details = ex.Message });
            }
        }

        [HttpPost("save-session-note")]
        public async Task<IActionResult> SaveSessionNote([FromBody] SaveSessionNoteCommand command)
        {
            if( command == null )
            {
                return BadRequest(new { Message = "Note data is required" });
            }

            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPost("save-emotional-status")]
        public async Task<IActionResult> SaveEmotionalStatus([FromBody] SaveEmotionalStatusCommand command)
        {
            if( command == null )
            {
                return BadRequest(new { Message = "Emotional status data is required" });
            }

            var result = await _mediator.Send(command);

            if( result )
            {
                return Ok(new { Message = "Your emotional status has been saved successfully in your progress tracker" });
            }

            return BadRequest(new { Message = "Failed to save emotional status. Please check the Appointment ID" });
        }
    }

}

