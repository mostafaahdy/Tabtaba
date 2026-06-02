using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.Auth;

namespace Tabtaba.Services.Features.AuthenticationServices;

public class RegisterTherapistHandler : IRequestHandler<RegisterTherapistCommand, RegisterResponse>
{
    private readonly UserManager<User> _userManager;

    public RegisterTherapistHandler(UserManager<User> userManager)
        => _userManager = userManager;

    public async Task<RegisterResponse> Handle(
        RegisterTherapistCommand request,
        CancellationToken cancellationToken)
    {
        //  Email 
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Email already exists.");

        //  User
        var user = new User
        {
            FullName = request.FullName,
            LastName = request.L_Name,
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.Phone,
            Gender = request.Gender,
            UserType = nameof(UserRole.Therapist),
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        //  Role
        await _userManager.AddToRoleAsync(user, nameof(UserRole.Therapist));

        return new RegisterResponse
        {
            Email = user.Email,
            Role = nameof(UserRole.Therapist),
            Message = "Therapist registered successfully."
        };
    }
}