using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Therapist
{
    public class SlotGroupDTO
    {
        public string GroupName { get; set; } = default!; // "Afternoon "Evening"
        public List<SlotDetailDTO> Slots { get; set; } = new();
    }
}
