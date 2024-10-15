using BlogPlatform.Common.Extensions;
using BlogPlatform.Features.Rates.Common;
using BlogPlatform.Features.Rates.Parameters;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Features.Rates
{
    public class RateEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var ratesGroup = app.MapGroup("/rates")
                .WithTags("Rates");

            ratesGroup.MapPost("/add",
                async ([FromBody] AddRateRequest request, RateService _service, CancellationToken cancellationToken) =>
                {
                    await _service.AddRateAsync(request, cancellationToken);
                    return Results.Ok("Rate added successfully!");
                }).Validator<AddRateRequest>();

            ratesGroup.MapDelete("/delete/{rateId:int}",
                async (int rateId, RateService _service, CancellationToken cancellationToken) =>
                {
                    await _service.DeleteRateAsync(rateId, cancellationToken);
                    return Results.Ok("Rate deleted successfully!");
                });
        }
    }
}
