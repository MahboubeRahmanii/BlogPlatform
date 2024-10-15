using BlogPlatform.Features.Rates.Parameters;
using FluentValidation;

namespace BlogPlatform.Features.Rates.Validator
{
    public class AddRateRequestValidator : AbstractValidator<AddRateRequest>
    {
        public AddRateRequestValidator()
        {
            RuleFor(x => x.RateNumber)
            .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}
