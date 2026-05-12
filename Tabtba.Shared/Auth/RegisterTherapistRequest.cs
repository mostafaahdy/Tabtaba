using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tabtaba.Shared.Auth;

public class RegisterTherapistRequest
{
    public string FullName { get; set; } = default!;
    public string L_Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string Specialization { get; set; } = default!;
    public int YearsOfExperience { get; set; }
    public string LicenseNumber { get; set; } = default!;
}