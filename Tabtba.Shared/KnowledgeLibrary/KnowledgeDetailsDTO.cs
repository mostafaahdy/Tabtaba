using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.KnowledgeLibrary
{
    public class KnowledgeDetailsDTO :KnowledgeItemDTO
    {
        public string VideoUrl { get; set; } = default!;
        public string ShareUrl { get; set; } = default!;
        public List<VideoChapterDTO> Chapters { get; set; } = new();
        public List<CommentDTO> Comments { get; set; } = new();
        public List<KnowledgeItemDTO> SuggestedContent { get; set; } = new();
    }
}
