using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.KnowledgeLibrary
{
    public class KnowledgeLibraryResponse
    {
        public List<KnowledgeItemDTO> KnowledgeItems { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
