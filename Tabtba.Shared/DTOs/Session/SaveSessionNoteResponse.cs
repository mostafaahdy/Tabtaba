using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.DTOs.Session;

public class SaveSessionNoteResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime LastSaved { get; set; }
}