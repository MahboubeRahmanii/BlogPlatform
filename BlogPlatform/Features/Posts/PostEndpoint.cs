using BlogPlatform.Common.Extensions;
using BlogPlatform.Features.Posts.Common;
using BlogPlatform.Features.Posts.Parameters;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Features.Posts
{
    public class PostEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var postsGroup = app.MapGroup("/posts")
                .WithTags("Posts");

            postsGroup.MapPost("/add",
                async ([FromBody] AddPostRequest request, PostService _service, CancellationToken cancellationToken) =>
                {
                    await _service.AddPostAsync(request, cancellationToken);
                    return Results.Ok("Post added successfully!");
                }).Validator<AddPostRequest>();

            postsGroup.MapDelete("/delete/{postId:int}",
            async (int postId, PostService _service, CancellationToken cancellationToken) =>
            {
                await _service.DeletePostAsync(postId, cancellationToken);
                return Results.Ok("Post deleted successfully!");
            });

            postsGroup.MapPut("/edit/{postId:int}",
            async ([FromBody] EditPostRequest request, int postId, PostService _service, CancellationToken cancellationToken) =>
            {
                await _service.EditPostAsync(postId, request, cancellationToken);
                return Results.Ok("Post edited successfully!");
            }).Validator<EditPostRequest>();

            postsGroup.MapGet("/all",
            async (PostService _service, CancellationToken cancellationToken) =>
            {
                var posts = await _service.GetAllPostsAsync(cancellationToken);
                return posts.Any() ? Results.Ok(posts) : Results.NotFound("No posts found");
            });

            postsGroup.MapGet("/get/{postId}",
            async (int postId, PostService _service, CancellationToken cancellationToken) =>
            {
                var post = await _service.GetPostByPostIdAsync(postId, cancellationToken);
                return post is not null ? Results.Ok(post) : Results.NotFound("Post not found");
            });

            postsGroup.MapGet("/getPostsOfUser/{userId}",
            async (int userId, PostService _service, CancellationToken cancellationToken) =>
            {
                var post = await _service.GetPostsByUserIdAsync(userId, cancellationToken);
                return post is not null ? Results.Ok(post) : Results.NotFound("Post not found");
            });

            postsGroup.MapGet("/getVersionsOfPost/{postId}",
            async (int postId, PostService _service, CancellationToken cancellationToken) =>
            {
                var versions = await _service.GetVersionsOfPostAsync(postId, cancellationToken);
                return versions is not null ? Results.Ok(versions) : Results.NotFound("versions not found");
            });
        }
    }
}
