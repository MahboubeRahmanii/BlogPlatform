using BlogPlatform.Features.Comments;
using BlogPlatform.Features.Posts;

namespace BlogPlatform.Features.Users
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
