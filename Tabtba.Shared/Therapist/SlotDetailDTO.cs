using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Therapist
{
    public class SlotDetailDTO
    {
        public string Time { get; set; } = default!; 
        public bool IsAvailable { get; set; } 
        public bool IsSelected { get; set; } = false; 
    }
}
