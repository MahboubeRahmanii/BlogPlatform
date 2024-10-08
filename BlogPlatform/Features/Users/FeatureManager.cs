using BlogPlatform.Features.Users.Common;
using BlogPlatform.Features.Users.Parameters;
using BlogPlatform.Features.Users.Validators;
using FluentValidation;
using ServiceCollector.Abstractions;

namespace BlogPlatform.Features.Users
{
    public class FeatureManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<UserService>();
            serviceCollection.AddValidatorsFromAssemblyContaining<AddUserRequest>();
            serviceCollection.AddScoped<IValidator<AddUserRequest>,AddUserRequestValidator>();
        }
    }
}
