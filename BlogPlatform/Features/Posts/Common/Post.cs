using BlogPlatform.Features.Comments.Common;
using BlogPlatform.Features.Rates;

namespace BlogPlatform.Features.Posts.Common
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ScheduledPublishDate { get; set; }
        public bool IsPublished { get; set; } = false;

        public int UserId { get; set; }

        public List<Comment> Comments { get; set; } = new();
        public List<PostVersion> PostVersions { get; set; } = new();
        public List<Rate> Rates { get; set; } = new();
    }
}
