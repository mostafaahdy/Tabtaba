using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.Enums;

namespace Tabtaba.Shared.Requests.IdentityRequests
{
    public record RegisterDTO(
        string DisplayName,
        string Email,
        string Password,
        string ConfirmPassword,
        DateTime BirthDate,
        UserRole Role,
        string? GoogleToken = null
    );
    
}
