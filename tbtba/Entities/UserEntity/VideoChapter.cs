namespace Tabtaba.Domain.Entities.UserEntity
{
    public class VideoChapter
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Timestamp { get; set; } = default!; 
        public int KnowledgeId { get; set; }
        public virtual KnowledgeLibrary Knowledge { get; set; } = default!;
    }
}