using BlogPlatform.Common;
using BlogPlatform.Features.Posts.Parameters;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Posts.Common
{
    public class PostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPostAsync(AddPostRequest request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePostAsync(int postId, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(new object[] { postId }, cancellationToken);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task EditPostAsync(int postId, EditPostRequest request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(new object[] { postId }, cancellationToken);
            if (post != null)
            {
                post.Title = request.Title;
                post.Content = request.Content;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Post>> GetAllPostsAsync(CancellationToken cancellationToken)
        {
            return await _context.Posts.ToListAsync(cancellationToken);
        }

        public async Task<Post?> GetPostByPostIdAsync(int postId, CancellationToken cancellationToken)
        {
            return await _context.Posts
                                 .FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
