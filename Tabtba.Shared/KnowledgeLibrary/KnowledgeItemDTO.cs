using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.KnowledgeLibrary
{
    public class KnowledgeItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string Category { get; set; } = default!;
        public string? ContentType { get; set; } =default!;
        public string? AuthorName { get; set; }
        public string? AuthorImageUrl { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
        public bool IsSaved { get; set; }
        public string? ReadTime { get; set; }
        public string? ViewsCount { get; set; }
        public string? CreatedAt { get; set; }
      
    }

}
