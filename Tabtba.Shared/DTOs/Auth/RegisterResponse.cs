using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Auth;

public class RegisterResponse
{
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Message { get; set; } = default!;
}