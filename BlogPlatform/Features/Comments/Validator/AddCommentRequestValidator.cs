using BlogPlatform.Features.Comments.Parameters;
using FluentValidation;

namespace BlogPlatform.Features.Comments.Validator
{
    public class AddCommentRequestValidator : AbstractValidator<AddCommentRequest>
    {
        public AddCommentRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters");
        }
    }
}
