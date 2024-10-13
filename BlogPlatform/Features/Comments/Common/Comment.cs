using BlogPlatform.Features.Posts.Common;
using BlogPlatform.Features.Users.Common;

namespace BlogPlatform.Features.Comments.Common
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PostId { get; set; }
        public Post Post { get; set; } = default!;

        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
