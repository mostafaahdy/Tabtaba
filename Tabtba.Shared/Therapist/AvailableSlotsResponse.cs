using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Therapist
{
    public class AvailableSlotsResponse
    {
        public DateTime SelectedDate { get; set; }
        public List<SlotGroupDTO> SlotGroups { get; set; } = new();
    }
}
