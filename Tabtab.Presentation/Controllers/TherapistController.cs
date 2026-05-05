using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabtaba.ServicesAbstraction.Queries;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TherapistController : ControllerBase
{
    private readonly IMediator _mediator;

    public TherapistController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("{therapistId}")]
    public async Task<IActionResult> GetProfile(Guid therapistId)
    {
        var result = await _mediator.Send(new GetTherapistProfileQuery(therapistId));
        return Ok(result);
    }
}