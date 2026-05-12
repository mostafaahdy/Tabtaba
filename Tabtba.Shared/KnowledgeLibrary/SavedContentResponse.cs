using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.KnowledgeLibrary
{
    public class SavedContentResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty; // "Video"  "Article"
        public long Views { get; set; }
        public DateTime SavedAt { get; set; }
    }
}
