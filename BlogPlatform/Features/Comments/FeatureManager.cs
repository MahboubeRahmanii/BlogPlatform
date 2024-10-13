using BlogPlatform.Features.Comments.Common;
using BlogPlatform.Features.Comments.Parameters;
using BlogPlatform.Features.Comments.Validator;
using FluentValidation;
using ServiceCollector.Abstractions;

namespace BlogPlatform.Features.Comments
{
    public class FeatureManager: IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<CommentService>();
            serviceCollection.AddValidatorsFromAssemblyContaining<AddCommentRequest>();
            serviceCollection.AddScoped<IValidator<AddCommentRequest>, AddCommentRequestValidator>();
        }
    }
}
