using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared;

public class EducationRequest
{
    public Guid TherapistId { get; set; }
    public string HighestDegree { get; set; } = string.Empty;
    public int GraduationYear { get; set; }
    public string UniversityName { get; set; } = string.Empty;
}