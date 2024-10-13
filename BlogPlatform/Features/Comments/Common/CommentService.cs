using BlogPlatform.Common;
using BlogPlatform.Features.Comments.Parameters;
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

            await _context.Comments.AddAsync(comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
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
