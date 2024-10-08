using BlogPlatform.Features.Users.Parameters;
using FluentValidation;

namespace BlogPlatform.Features.Users.Validators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.userName)
                .NotEmpty().WithMessage("UserName is required")
                .MaximumLength(50).WithMessage("UserName cannot exceed 50 characters");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.passwordHash)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
