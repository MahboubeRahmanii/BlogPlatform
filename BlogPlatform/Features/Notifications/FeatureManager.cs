using BlogPlatform.Features.Notifications.Common;
using ServiceCollector.Abstractions;

namespace BlogPlatform.Features.Notifications
{
    public class FeatureManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<NotificationService>();
        }
    }
}
