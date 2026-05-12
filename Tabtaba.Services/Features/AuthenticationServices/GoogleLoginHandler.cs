using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.Auth;

namespace Tabtaba.Services.Features.AuthenticationServices;

public class GoogleLoginHandler : IRequestHandler<GoogleLoginCommand, LoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public GoogleLoginHandler(
        UserManager<User> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Handle(
        GoogleLoginCommand request,
        CancellationToken cancellationToken)
    {
        // 1️⃣ شوف لو الـ User موجود
        var user = await _userManager.FindByEmailAsync(request.Email);

        // 2️⃣ لو مش موجود → اعمله
        if (user is null)
        {
            user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.Name,
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
                throw new Exception("Failed to create user.");
        }

        // 3️⃣ جيب الـ Role
        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "User";

        // 4️⃣ ارجع JWT
        var token = GenerateJwtToken(user, role);
        return new LoginResponse
        {
            Token = token,
            Email = user.Email ?? string.Empty,
            Role = role,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
    }

    private string GenerateJwtToken(User user, string role)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email,           user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role,            role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}