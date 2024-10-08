using BlogPlatform.Common;
using BlogPlatform.Features.Users.Parameters;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Features.Users.Common
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(AddUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.userName,
                Email = request.email,
                PasswordHash = request.passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task EditUserAsync(int userId, EditUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(userId , cancellationToken);
            if (user != null)
            {
                user.UserName = request.userName;
                user.Email = request.email;
                user.PasswordHash = request.passwordHash;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _context.Users
                                 .Where(u => u.UserName == userName)
                                 .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
