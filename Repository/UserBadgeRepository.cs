using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;

namespace TaskApp_Web.Repositories
{
    public class UserBadgeRepository : IUserBadgeRepository
    {
        private readonly TaskAppContext _context;

        public UserBadgeRepository(TaskAppContext context)
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
