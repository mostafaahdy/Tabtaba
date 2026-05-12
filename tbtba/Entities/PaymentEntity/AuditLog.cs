using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.BaymentgatewayEntity;

public class AuditLog
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Action { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
