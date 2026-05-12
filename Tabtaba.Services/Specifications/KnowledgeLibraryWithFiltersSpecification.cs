using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Services.Specifications
{
    public class KnowledgeLibraryWithFiltersSpecification :BaseSpecifications<KnowledgeLibrary>
    {
        public KnowledgeLibraryWithFiltersSpecification(string? contentType,string? category)
            : base(x =>
                (string.IsNullOrEmpty(contentType) || string.Equals(x.ContentType,contentType,StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(category) || string.Equals(x.Category,category,StringComparison.OrdinalIgnoreCase))
            )
        {

            AddOrderByDescending(x => x.CreatedAt);

            AddInclude(x => x.Doctor!);
            AddInclude($"{nameof(KnowledgeLibrary.Doctor)}.{nameof(Doctor.User)}");
        }

        public KnowledgeLibraryWithFiltersSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.Doctor!);
            AddInclude($"{nameof(KnowledgeLibrary.Doctor)}.{nameof(Doctor.User)}");
            AddInclude(x => x.Likes);
            AddInclude(x => x.SavedByUsers);
            AddInclude(x => x.Doctor!.User);
        }
    }
}
