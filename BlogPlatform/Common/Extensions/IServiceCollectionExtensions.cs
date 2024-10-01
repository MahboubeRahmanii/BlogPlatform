using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Common.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=blogplatform.db"));

            return services;
        }
    }
}
