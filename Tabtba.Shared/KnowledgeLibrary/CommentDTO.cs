using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.KnowledgeLibrary
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string UserImageUrl { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string CreatedAt { get; set; } = default!;
        public int LikesCount { get; set; }
    }
}
