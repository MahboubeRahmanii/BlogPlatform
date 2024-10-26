using BlogPlatform.Common;
using BlogPlatform.Features.Notifications;
using BlogPlatform.Features.Notifications.Common;
using BlogPlatform.Features.Rates.Parameters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Rates.Common
{
    public class RateService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<RateService> _logger;

        public RateService(AppDbContext context, IHubContext<NotificationHub> hubContext, ILogger<RateService> logger)
        {
            _context = context;
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task AddRateAsync(AddRateRequest request, CancellationToken cancellationToken)
        {
            var rate = new Rate
            {
                UserId = request.UserId,
                PostId = request.PostId,
                RateNumber = request.RateNumber,
            };

            var post = await _context.Posts.Include(p => p.Rates)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post != null && post.UserId != request.UserId)
            {
                var notification = new Notification
                {
                    UserId = post.UserId,
                    Message = $"User {request.UserId} rated your post '{post.Title}'",
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);
                await _hubContext.Clients.User(post.UserId.ToString()).SendAsync("ReceiveNotification", notification.Message);
                post.Rates.Add(rate);
                await _context.Rates.AddAsync(rate, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Notification sent: {notification.Message}");
            }
        }

        public async Task DeleteRateAsync(int rateId, CancellationToken cancellationToken)
        {
            var rate = await _context.Rates.FindAsync(new object[] { rateId }, cancellationToken);

            if (rate == null)
            {
                throw new ArgumentException("Rate not found");
            }

            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
