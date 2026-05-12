using Tabtaba.Domain.Entities.Enums;
using Tabtaba.Domain.Enums;

namespace Tabtaba.Shared.Auth;

public class SelectRoleRequest
{
    public UserRole Role { get; set; }
}