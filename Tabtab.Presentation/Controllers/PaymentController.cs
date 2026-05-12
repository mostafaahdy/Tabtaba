using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.Payment;

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
    } //  fawrypay 

    [HttpPost("fawry-pay")]
    public async Task<IActionResult> FawryPay([FromBody] FawryPayRequest request)
    {
        try
        {
            var command = new FawryPayCommand(
                request.PatientId,
                request.AppointmentId);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpGet("transaction/{paymentId}")]
    public async Task<IActionResult> GetTransaction(int paymentId)
    {
        try
        {
            var result = await _mediator.Send(new GetTransactionQuery(paymentId));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}