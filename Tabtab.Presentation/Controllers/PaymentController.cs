using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("add-card")]
    public async Task<IActionResult> AddCard([FromBody] AddCardRequest request)
    {
        var command = new AddCardCommand(
            request.CardHolderName,
            request.CardNumber,
            request.ExpiryDate,
            request.UserId);

        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPost("vodafone-pay")]
    public async Task<IActionResult> VodafonePay([FromBody] VodafonePayRequest request)
    {
       
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new VodafonePayCommand(
                request.PatientId,
                request.WalletPhoneNumber,
                request.AppointmentId);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)      
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex) 
        {
            return Conflict(new { message = ex.Message });
        }
    }
}