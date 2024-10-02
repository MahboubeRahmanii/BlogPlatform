using BlogPlatform.Features.Posts;
using BlogPlatform.Features.Users;

namespace BlogPlatform.Features.Rates
{
    public class Rate
    {
        public int RateId { get; set; }         
        public int RateNumber { get; set; }     
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }         
        public int PostId { get; set; }
        public User User { get; set; } = default!;
        public Post Post { get; set; } = default!;
    }
}
