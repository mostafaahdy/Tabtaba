using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
   public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;
        public int KnowledgeId { get; set; }
        public virtual KnowledgeLibrary Knowledge { get; set; } = default!;
    }
}
