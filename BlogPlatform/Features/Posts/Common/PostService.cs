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
                var postVersion = new PostVersion
                {
                    PostId = post.Id,
                    Title = request.Title,
                    Content = request.Content,
                    VersionNumber = await GetNextVersionNumberAsync(post.Id, cancellationToken)
                };

                await _context.PostVersions.AddAsync(postVersion, cancellationToken);

                post.Title = request.Title;
                post.Content = request.Content;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Post>> GetAllPostsAsync(CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Rates)
                .ToListAsync(cancellationToken);
        }

        public async Task<Post?> GetPostByPostIdAsync(int postId, CancellationToken cancellationToken)
        {
            return await _context.Posts.Include(p => p.Comments)
                                 .Include(p => p.Rates)
                                 .FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Posts.Include(p => p.Comments).Include(p => p.Rates).Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<List<PostVersion>> GetVersionsOfPostAsync(int postId, CancellationToken cancellationToken)
        {
            var versions = await _context.PostVersions
                .Where(pv => pv.PostId == postId)
                .OrderBy(pv => pv.VersionNumber)  
                .ToListAsync(cancellationToken);

            return versions;
        }

        private async Task<int> GetNextVersionNumberAsync(int postId, CancellationToken cancellationToken)
        {
            return await _context.PostVersions
                                 .Where(pv => pv.PostId == postId)
                                 .CountAsync(cancellationToken) + 1;
        }
    }
}
