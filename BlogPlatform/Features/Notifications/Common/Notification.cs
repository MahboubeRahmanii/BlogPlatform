using BlogPlatform.Features.Users.Common;

namespace BlogPlatform.Features.Notifications.Common
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }  
        public string Message { get; set; } 
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false; 

        public User User { get; set; } = default!;
    }
}
