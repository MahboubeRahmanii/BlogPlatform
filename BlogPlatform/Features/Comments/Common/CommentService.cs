using BlogPlatform.Common;
using BlogPlatform.Features.Comments.Parameters;
using BlogPlatform.Features.Notifications.Common;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Comments.Common
{
    public class CommentService
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(AddCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            var post = await _context.Posts.Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post != null && post.UserId != request.UserId)  
            {
                var notification = new Notification
                {
                    UserId = post.UserId,
                    Message = $"User {request.UserId} commented on your post '{post.Title}'",
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);
                post.Comments.Add(comment);
                await _context.Comments.AddAsync(comment, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteCommentAsync(int commentId, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FindAsync(new object[] { commentId }, cancellationToken);

            if (comment == null)
            {
                throw new ArgumentException("Comment not found");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId, CancellationToken cancellationToken)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
