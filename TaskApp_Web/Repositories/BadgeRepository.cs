using TaskApp_Web.Models;
using TaskApp_Web.Data;
using TaskApp_Web.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApp_Web.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly TaskApp_WebContext _context;

        public BadgeRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Badge>> GetAllBadgesAsync()
        {
            return await _context.Badges.ToListAsync();
        }

        public async Task<Badge> GetBadgeByIdAsync(int badgeId)
        {
            return await _context.Badges.FindAsync(badgeId);
        }

        public async Task<bool> AddBadgeAsync(Badge badge)
        {
            _context.Badges.Add(badge);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBadgeAsync(Badge badge)
        {
            _context.Badges.Update(badge);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBadgeAsync(int badgeId)
        {
            var badge = await _context.Badges.FindAsync(badgeId);
            if (badge != null)
            {
                _context.Badges.Remove(badge);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Badge>> GetBadgesByTaskCompletionCountAsync(int taskCompletionCount)
        {
            return await _context.Badges
                .Where(b => b.TaskCompletionThreshold <= taskCompletionCount)
                .ToListAsync();
        }

        // Yeni metodun tanımı
        public async Task<IEnumerable<Badge>> GetAvailableBadgesAsync(int userId)
        {
            // Kullanıcının mevcut rozetlerini almak için UserBadges tablosunu kontrol edin
            var userBadges = await _context.UserBadges
                .Where(ub => ub.UserId == userId)
                .Select(ub => ub.BadgeId)
                .ToListAsync();

            // Mevcut rozetler listesine göre kullanıcının kazanabileceği rozetleri döndür
            return await _context.Badges
                .Where(b => !userBadges.Contains(b.BadgeId))
                .ToListAsync();
        }
    }
}
