using BlogPlatform.Features.Notifications.Common;
using Carter;

namespace BlogPlatform.Features.Notifications
{
    public class NotificationEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var notificationsGroup = app.MapGroup("/notifications")
                .WithTags("Notifications");

            notificationsGroup.MapGet("/{userId:int}", async (int userId, NotificationService notificationService, CancellationToken cancellationToken) =>
            {
                var notifications = await notificationService.GetNotificationsForUserAsync(userId, cancellationToken);
                return notifications.Any() ? Results.Ok(notifications) : Results.NotFound("No unread notifications found.");
            });

            notificationsGroup.MapPut("/markAsRead/{notificationId:int}", async (int notificationId, NotificationService notificationService, CancellationToken cancellationToken) =>
            {
                await notificationService.MarkAsReadAsync(notificationId, cancellationToken);
                return Results.Ok("Notification marked as read.");
            });
        }
    }
}
