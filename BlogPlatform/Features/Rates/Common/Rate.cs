using BlogPlatform.Features.Posts.Common;
using BlogPlatform.Features.Users.Common;

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
    }
}
