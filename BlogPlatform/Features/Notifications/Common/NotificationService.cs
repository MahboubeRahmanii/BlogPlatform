using BlogPlatform.Common;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Notifications.Common
{
    public class NotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetNotificationsForUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task MarkAsReadAsync(int notificationId, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FindAsync(notificationId, cancellationToken);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
