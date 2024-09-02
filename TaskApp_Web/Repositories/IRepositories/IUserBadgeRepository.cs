using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories.IRepositories
{
    public interface IUserBadgeRepository
    {
        Task<IEnumerable<UserBadge>> GetUserBadgesAsync(int userId);
        Task<bool> UserHasBadgeAsync(int userId, int badgeId);
        Task AddUserBadgeAsync(UserBadge userBadge);
    }
}
