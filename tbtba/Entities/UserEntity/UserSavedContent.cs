using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class UserSavedContent
    {
        public string UserId { get; set; } = default!; 
        public int PatientId { get; set; }
        public int KnowledgeLibraryId { get; set; }

        // Navigation Properties
        public KnowledgeLibrary KnowledgeLibrary { get; set; } = default!;
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
