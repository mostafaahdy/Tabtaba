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
using Tabtaba.Shared.DTOs.Earnings;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EarningsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EarningsController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("{doctorId}")]
    public async Task<IActionResult> GetEarnings(int doctorId)
    {
        var result = await _mediator.Send(new GetEarningsQuery(doctorId));
        return Ok(result);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
    {
        try
        {
            var command = new WithdrawCommand(
                request.DoctorId,
                request.Amount,
                request.PaymentMethod);

            await _mediator.Send(command);
            return Ok(new { message = "Withdrawal request submitted successfully." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}