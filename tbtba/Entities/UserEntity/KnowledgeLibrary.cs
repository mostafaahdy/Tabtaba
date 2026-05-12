using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.TherapistEntity;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class KnowledgeLibrary
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public string Category { get; set; } = default!;
        public string ContentType { get; set; } = default!; // Video or Article
        public int ReadTimeMinutes { get; set; }
        public string? VideoUrl { get;  set; }
        public long ViewsCount { get; set; }

        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

         //One-to-Many
        public virtual ICollection<VideoChapter> Chapters { get; set; } = new HashSet<VideoChapter>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<KnowledgeLike> Likes { get; set; } = new HashSet<KnowledgeLike>();
        public virtual ICollection<UserSavedContent> SavedByUsers { get; set; } = new HashSet<UserSavedContent>();


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
