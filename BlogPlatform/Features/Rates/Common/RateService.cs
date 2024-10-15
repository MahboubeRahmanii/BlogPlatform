using BlogPlatform.Common;
using BlogPlatform.Features.Rates.Parameters;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Rates.Common
{
    public class RateService
    {
        private readonly AppDbContext _context;

        public RateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRateAsync(AddRateRequest request, CancellationToken cancellationToken)
        {
            var rate = new Rate
            {
                UserId = request.UserId,
                PostId = request.PostId,
                RateNumber = request.RateNumber,
            };

            var post = await _context.Posts.Include(p => p.Rates)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post != null)
            {
                post.Rates.Add(rate);
                await _context.Rates.AddAsync(rate, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteRateAsync(int rateId, CancellationToken cancellationToken)
        {
            var rate = await _context.Rates.FindAsync(new object[] { rateId }, cancellationToken);

            if (rate == null)
            {
                throw new ArgumentException("Rate not found");
            }

            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
