using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Session
{
    public class ReviewResponse
    {
        public string Title { get; set; } = "Feedback Submitted !";
        public string SubTitle { get; set; } = "Thank you for helping us improve our service";
        public string DoctorName { get; set; } = default!;
        public bool IsSuccess { get; set; } = true;
    }
}
