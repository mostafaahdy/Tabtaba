using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public class CreateLibraryContentCommand :IRequest<int>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string Category { get; set; } = default!; // ADHD, Autism, etc.
        public string ContentType { get; set; } = default!; // Article or Video
        public int ReadTimeMinutes { get; set; }
        public int? DoctorId { get; set; } 
    }
    
}
