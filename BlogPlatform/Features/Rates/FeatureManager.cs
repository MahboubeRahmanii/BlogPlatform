using BlogPlatform.Features.Rates.Common;
using BlogPlatform.Features.Rates.Parameters;
using BlogPlatform.Features.Rates.Validator;
using FluentValidation;
using ServiceCollector.Abstractions;

namespace BlogPlatform.Features.Rates
{
    public class FeatureManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<RateService>();
            serviceCollection.AddValidatorsFromAssemblyContaining<AddRateRequest>();
            serviceCollection.AddScoped<IValidator<AddRateRequest>, AddRateRequestValidator>();
        }
    }
}
