using BlogPlatform.Common.Extensions;
using BlogPlatform.Features.Comments.Common;
using BlogPlatform.Features.Comments.Parameters;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Features.Comments
{
    public class Endpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var commentsGroup = app.MapGroup("/comments")
                .WithTags("Comments");

            commentsGroup.MapPost("/add",
            async ([FromBody] AddCommentRequest request, CommentService _service, CancellationToken cancellationToken) =>
            {
                await _service.AddCommentAsync(request, cancellationToken);
                return Results.Ok("Comment added successfully!");
            }).Validator<AddCommentRequest>();

            commentsGroup.MapDelete("/delete/{commentId:int}",
            async (int commentId, CommentService _service, CancellationToken cancellationToken) =>
            {
                await _service.DeleteCommentAsync(commentId, cancellationToken);
                return Results.Ok("Comment deleted successfully!");
            });

            commentsGroup.MapGet("/post/{postId:int}",
            async (int postId, CommentService _service, CancellationToken cancellationToken) =>
            {
                var comments = await _service.GetCommentsByPostIdAsync(postId, cancellationToken);
                return comments.Any() ? Results.Ok(comments) : Results.NotFound("No comments found for this post");
            });
        }
    }
}
