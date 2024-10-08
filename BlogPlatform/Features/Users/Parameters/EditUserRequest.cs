namespace BlogPlatform.Features.Users.Parameters
{
    public record EditUserRequest(string userName, string email, string passwordHash);

}
