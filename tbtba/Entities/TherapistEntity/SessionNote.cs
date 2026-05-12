using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Domain.Entities.TherapistEntity;

public class SessionNote
{
    public int Id { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime Date_Created { get; set; } = DateTime.UtcNow;
    public DateTime Last_Updated { get; set; } = DateTime.UtcNow;
    public int AppointmentId { get; set; }
    public virtual Appointment Appointment { get; set; } = default!;
}
