﻿using BlogPlatform.Features.Posts.Parameters;
using FluentValidation;

namespace BlogPlatform.Features.Posts.Validator
{
    public class EditPostRequestValidator : AbstractValidator<EditPostRequest>
    {
        public EditPostRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters");
        }
    }
}
