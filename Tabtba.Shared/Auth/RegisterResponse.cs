using System;

namespace Tabtaba.Shared.Auth;

public class RegisterResponse
{
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Message { get; set; } = default!;

    // 🚀 الحقول السحرية اللي الفرونت إند مستنيها عشان يثبت الجلسة ويرفع الملفات
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public Guid? TherapistGuid { get; set; }
}