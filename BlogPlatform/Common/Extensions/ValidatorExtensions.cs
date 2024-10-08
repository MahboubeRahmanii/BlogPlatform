using BlogPlatform.Common.Filters;

namespace BlogPlatform.Common.Extensions
{
    public static class ValidatorExtensions
    {
        public static RouteHandlerBuilder Validator<T>(this RouteHandlerBuilder handlerBuilder)
        where T : class
        {
            handlerBuilder.AddEndpointFilter<EndpointValidatorFilter<T>>();
            return handlerBuilder;
        }
    }
}
