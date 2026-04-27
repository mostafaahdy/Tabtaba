using Microsoft.AspNetCore.Mvc;
using Tabtaba.Application.DTOs.Auth;
using Tabtaba.Domain.Enums;

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    /// 
    [HttpPost("select-role")]
    public IActionResult SelectRole([FromBody] SelectRoleRequest request)
    {
        if (!Enum.IsDefined(typeof(UserRole), request.Role))
            return BadRequest(new { message = "Invalid role selected." });

        
        var redirectInfo = request.Role switch
        {
            UserRole.Patient => new
            {
                role = nameof(UserRole.Patient),
                nextStep = "/api/auth/register/patient"
            },
            UserRole.Therapist => new
            {
                role = nameof(UserRole.Therapist),
                nextStep = "/api/auth/register/therapist"
            },
            _ => null
        };

        return Ok(new
        {
            message = "Role selected successfully.",
            data = redirectInfo
        });
    }
}