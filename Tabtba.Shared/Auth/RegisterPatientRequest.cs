using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Auth;

public class RegisterPatientRequest
{
    public string FullName { get; set; } = default!;
    public string L_Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string DateOfBirth { get; set; } = default!; public string MaritalStatus { get; set; } = default!;
}