using BlogPlatform.Features.Users.Common;
using Carter;
using Microsoft.AspNetCore.Mvc;
using BlogPlatform.Common.Extensions;
using BlogPlatform.Features.Users.Parameters;

namespace BlogPlatform.Features.Users
{
    public class Endpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGroup("/users")
                .WithTags("Users")

                .MapPost("/add",
                async ([FromBody] AddUserRequest request, UserService _service, CancellationToken cancellationToken) =>
                {
                    await _service.AddUserAsync(request, cancellationToken);
                    return Results.Ok("User added successfully!");
                }).Validator<AddUserRequest>();

            app.MapDelete("/delete/{userId:int}",
            async (int userId, UserService _service, CancellationToken cancellationToken) =>
            {
                await _service.DeleteUserAsync(userId, cancellationToken);
                return Results.Ok("User deleted successfully!");
            });

            app.MapPut("/edit/{userId:int}",
            async ([FromBody] EditUserRequest request, int userId, UserService _service, CancellationToken cancellationToken) =>
            {
                await _service.EditUserAsync(userId, request, cancellationToken);
                return Results.Ok("User edited successfully!");
            }).Validator<EditUserRequest>();

            app.MapGet("/all",
        async (UserService _service, CancellationToken cancellationToken) =>
        {
            var users = await _service.GetAllUsersAsync(cancellationToken);
            return users.Any() ? Results.Ok(users) : Results.NotFound("No users found");
        });

            app.MapGet("/get/{userName}",
            async (string userName, UserService _service, CancellationToken cancellationToken) =>
            {
                var user = await _service.GetUserByUserNameAsync(userName, cancellationToken);
                return user is not null ? Results.Ok(user) : Results.NotFound("User not found");
            });
        }
    }
}
