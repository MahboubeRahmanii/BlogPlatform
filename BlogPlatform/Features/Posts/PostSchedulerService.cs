using BlogPlatform.Common;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Posts
{
    public class PostSchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1); 

        public PostSchedulerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var postsToPublish = await context.Posts
                        .Where(p => p.ScheduledPublishDate <= DateTime.Now && !p.IsPublished)
                        .ToListAsync(stoppingToken);

                    foreach (var post in postsToPublish)
                    {
                        post.IsPublished = true;
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
