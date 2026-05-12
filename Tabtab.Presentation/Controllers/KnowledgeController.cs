using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.ServicesAbstraction.Queries.PatientQueries;
using Tabtaba.Shared.KnowledgeLibrary;

namespace Tabtaba.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnowledgeController :ControllerBase
    {
        private readonly IMediator _mediator;
        public KnowledgeController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<KnowledgeLibraryResponse>> GetList([FromQuery] GetKnowledgeListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost("{id}/increment-views")]
        public async Task<IActionResult> IncrementViews(int id)
        {
            var result = await _mediator.Send(new IncrementViewsCommand(id));

            if( !result ) return NotFound("Content not found");
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Therapist,Doctor")]
        public async Task<ActionResult<int>> Create([FromBody] CreateLibraryContentCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetVideoDetails),new { id = id },id);
        }

        [HttpPost("{id}/bookmark")]
        [Authorize]
        public async Task<IActionResult> ToggleBookmark(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                         ?? User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if( string.IsNullOrEmpty(userId) )
                return Unauthorized("You Must Be Logged In");

            var result = await _mediator.Send(new ToggleBookmarkCommand(id,userId));
            return Ok(new { message = result });
        }


        [HttpGet("saved")]
        [Authorize]
        public async Task<ActionResult<KnowledgeLibraryResponse>> GetMySavedContent([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                         ?? User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if( string.IsNullOrEmpty(userId) ) return Unauthorized();

            var result = await _mediator.Send(new GetUserSavedContentQuery(userId,pageNumber,pageSize));
            return Ok(result);
        }


        [HttpPost("{id}/like")]
        [Authorize]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                         ?? User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if( string.IsNullOrEmpty(userId) ) return Unauthorized();

            var result = await _mediator.Send(new ToggleLikeCommand(id,userId));

            return Ok(new { status = result });
        }

       
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<KnowledgeDetailsDTO>> GetVideoDetails(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                         ?? User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            
            var result = await _mediator.Send(new GetKnowledgeDetailsQuery(id,userId));

            if( result == null )
                return NotFound(new { message = "The Content Not Found" });

            return Ok(result);
        }
    }
    }
