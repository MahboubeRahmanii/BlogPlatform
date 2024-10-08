namespace BlogPlatform.Features.Users.Parameters
{
    public record AddUserRequest(string userName, string email, string passwordHash);

}
