namespace BlogPlatform.Features.Posts.Parameters
{
    public record AddPostRequest(string Title, string Content, int UserId, DateTime? ScheduledPublishDate);
}
