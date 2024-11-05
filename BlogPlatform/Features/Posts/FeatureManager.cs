using BlogPlatform.Features.Posts.Common;
using BlogPlatform.Features.Posts.Parameters;
using BlogPlatform.Features.Posts.Validator;
using FluentValidation;
using ServiceCollector.Abstractions;

namespace BlogPlatform.Features.Posts
{
    public class FeatureManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<PostService>();
            serviceCollection.AddHostedService<PostSchedulerService>();
            serviceCollection.AddValidatorsFromAssemblyContaining<AddPostRequest>();
            serviceCollection.AddValidatorsFromAssemblyContaining<EditPostRequest>();
            serviceCollection.AddScoped<IValidator<AddPostRequest>, AddPostRequestValidator>();
            serviceCollection.AddScoped<IValidator<EditPostRequest>, EditPostRequestValidator>();
        }
    }
}
