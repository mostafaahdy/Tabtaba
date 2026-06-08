namespace Tabtaba.Shared.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty; // 👈 ضيف ده
    public int? PatientId { get; set; }                  // 👈 ضيف ده
    public Guid? TherapistId { get; set; }               // 👈 ضيف ده
    public DateTime ExpiresAt { get; set; }
}