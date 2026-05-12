using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class KnowledgeLike
    {
        public string UserId { get; set; } = default!;
        public int KnowledgeId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
        public KnowledgeLibrary Knowledge { get; set; } = default!;
    }
}

