using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Auth;

public class ResendOtpRequest
{
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
}
