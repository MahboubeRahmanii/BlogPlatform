namespace BlogPlatform.Features.Comments.Parameters
{
    public record AddCommentRequest(int UserId, int PostId, string Content);
}
