using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tabtaba.Domain.Entities.UserEntity; //  الـ Namespace الحقيقي لليوزر عندك

namespace Tabtaba.Presentation.Controllers;

[ApiController]
[Authorize] // 🔒 حماية من الـ IDOR: لازم Token
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public class UpdateProfileRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public ProfileController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    // 🟢 GET: api/Profile/me
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
        {
            return Unauthorized(new { message = "Please log in again." });
        }

        return Ok(new
        {
            fullName = user.FullName,
            lastName = user.LastName,
            email = user.Email ?? string.Empty,
            phoneNumber = user.PhoneNumber ?? string.Empty
        });
    }

    // 🔵 PUT: api/Profile/me
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateProfileRequest request)
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
        {
            return Unauthorized(new { message = "Please log in again." });
        }

        // تنظيف المدخلات
        var fullName = (request.FullName ?? string.Empty).Trim();
        var lastName = (request.LastName ?? string.Empty).Trim();
        var phoneNumber = (request.PhoneNumber ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(lastName))
        {
            return BadRequest(new { message = "First Name and Last Name are required." });
        }

        //  التحديث في الحقول الحقيقية المفرودة في سوبابيز بالملي
        user.FullName = fullName;
        user.LastName = lastName;
        user.PhoneNumber = phoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                message = "Unable to update profile.",
                errors = result.Errors.Select(error => error.Description)
            });
        }

        return Ok(new
        {
            fullName = user.FullName,
            lastName = user.LastName,
            email = user.Email ?? string.Empty,
            phoneNumber = user.PhoneNumber ?? string.Empty
        });
    }

    // ميثود جلب اليوزر الآمنة من الـ Token
    private async Task<User?> GetCurrentUserAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrWhiteSpace(userId))
        {
            var byId = await _userManager.FindByIdAsync(userId);
            if (byId is not null) return byId;
        }

        var email = User.FindFirstValue(ClaimTypes.Email);
        return string.IsNullOrWhiteSpace(email) ? null : await _userManager.FindByEmailAsync(email);
    }
}