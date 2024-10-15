namespace BlogPlatform.Features.Posts.Common
{
    public class PostVersion
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PostId { get; set; }
        public Post Post { get; set; } = default!;
    }
}
