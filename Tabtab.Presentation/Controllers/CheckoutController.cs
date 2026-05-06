using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.DTOs.Checkout;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CheckoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckoutController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("plans")]
    public async Task<IActionResult> GetPlans()
    {
        var result = await _mediator.Send(new GetPlansQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
    {
        try
        {
            var command = new CheckoutCommand(
                request.PatientId,
                request.PlanId,
                request.PaymentMethod);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
