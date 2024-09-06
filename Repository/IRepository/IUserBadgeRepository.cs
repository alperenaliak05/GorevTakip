using Models;

namespace Repositories.IReporsitory
{
    public interface IUserBadgeRepository
    {
        Task<IEnumerable<UserBadge>> GetUserBadgesAsync(int userId);
        Task<bool> UserHasBadgeAsync(int userId, int badgeId);
        Task AddUserBadgeAsync(UserBadge userBadge);
    }
}
