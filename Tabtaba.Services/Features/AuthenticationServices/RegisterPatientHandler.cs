using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Entities;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Auth;

namespace Tabtaba.Services.Features.AuthenticationServices;

public class RegisterPatientHandler : IRequestHandler<RegisterPatientCommand, RegisterResponse>
{
    private readonly UserManager<User> _userManager;

    public RegisterPatientHandler(UserManager<User> userManager)
        => _userManager = userManager;

    public async Task<RegisterResponse> Handle(
        RegisterPatientCommand request,
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
            L_Name = request.L_Name,
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.Phone,
            Gender = request.Gender,
            User_Type = nameof(UserRole.Patient),
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        //  Role
        await _userManager.AddToRoleAsync(user, nameof(UserRole.Patient));

        return new RegisterResponse
        {
            Email = user.Email,
            Role = nameof(UserRole.Patient),
            Message = "Patient registered successfully."
        };
    }
}