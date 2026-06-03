using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities; // 🚀 عشان يلقط كلاس الـ Therapist لو مكانه هنا
using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.Auth;
using Tabtba.Persistence.Data.DbContexts; // 🚀 عشان يققرأ الـ DbContext بتاعك من الـ using اللي فوق عندك

namespace Tabtaba.Services.Features.AuthenticationServices;

public class RegisterTherapistHandler : IRequestHandler<RegisterTherapistCommand, RegisterResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context; // 🔒 البديل المضمون والسريع للـ UnitOfWork

    public RegisterTherapistHandler(
        UserManager<User> userManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<RegisterResponse> Handle(
        RegisterTherapistCommand request,
        CancellationToken cancellationToken)
    {
        // ── 1. فحص البريد الإلكتروني
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Email already exists.");

        // ── 2. إنشاء مستخدم الـ Identity
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

        // ── 3. إضافة الـ Role
        await _userManager.AddToRoleAsync(user, nameof(UserRole.Therapist));

        // ── 4. 🔥 الـحـل الـسـحـري: التخزين المباشر في الـ DbContext لتفادي مشاكل مسميات الـ Repository
        var therapist = new Therapist
        {
            Id = Guid.NewGuid(), // الـ ID السحري لـ Step 5
            UserId = user.Id,
            Specialization = request.Specialization,
            YearsOfExperience = request.YearsOfExperience,
            
        };

        await _context.Set<Therapist>().AddAsync(therapist, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken); // التسميع الفوري في سوبابيز

        // ── 5. إرجاع الـ Response بالـ TherapistGuid (التوكنات سيب الفرونت يسحبها أوتوماتيك من الـ Login بعد الـ Wizard)
        return new RegisterResponse
        {
            Email = user.Email,
            Role = nameof(UserRole.Therapist),
            Message = "Therapist registered successfully.",
            TherapistGuid = therapist.Id // 🔒 طلقة الرحمة للإيرور الأحمر في الـ Wizard!
        };
    }
}