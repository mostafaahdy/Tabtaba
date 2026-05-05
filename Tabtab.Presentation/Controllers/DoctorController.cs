using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.DTOs.Session;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DoctorController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(doctorId))
            return Unauthorized();

        var query = new GetDoctorDashboardQuery(doctorId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    // ── Save Session Notes ─────────────────────────────────────────────────
    [HttpPost("session-notes")]
    public async Task<IActionResult> SaveSessionNotes([FromBody] SaveSessionNoteRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new SaveSessionNoteCommand(
            request.AppointmentId,
            request.Notes
        );

        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(result);
    }

    // ── Save Emotional Status ──────────────────────────────────────────────
    [HttpPost("emotional-status")]
    public async Task<IActionResult> SaveEmotionalStatus([FromBody] SaveEmotionalStatusRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new SaveEmotionalStatusCommand(
            request.AppointmentId,
            request.MoodScore
        );

        var result = await _mediator.Send(command);

        if (!result)
            return BadRequest(new { message = "Failed to save emotional status." });

        return Ok(new { message = "Emotional status saved successfully." });
    }
    // ── Weekly Schedule ────────────────────────────────────────────────────
    [HttpGet("schedule")]
    public async Task<IActionResult> GetWeeklySchedule([FromQuery] DateTime weekStartDate)
    {
        var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(doctorId))
            return Unauthorized();

        var query = new GetWeeklyScheduleQuery(doctorId, weekStartDate);
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}