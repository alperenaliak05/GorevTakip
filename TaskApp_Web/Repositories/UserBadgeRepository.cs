using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories.IRepositories;

namespace TaskApp_Web.Repositories
{
    public class UserBadgeRepository : IUserBadgeRepository
    {
        private readonly TaskApp_WebContext _context;

        public UserBadgeRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserBadge>> GetUserBadgesAsync(int userId)
        {
            return await _context.UserBadges
                                 .Include(ub => ub.Badge)
                                 .Where(ub => ub.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<bool> UserHasBadgeAsync(int userId, int badgeId)
        {
            return await _context.UserBadges.AnyAsync(ub => ub.UserId == userId && ub.BadgeId == badgeId);
        }

        public async Task AddUserBadgeAsync(UserBadge userBadge)
        {
            await _context.UserBadges.AddAsync(userBadge);
            await _context.SaveChangesAsync();
        }
    }
}
