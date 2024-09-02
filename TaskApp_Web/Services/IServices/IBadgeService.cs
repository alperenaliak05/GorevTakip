using TaskApp_Web.Models;

namespace TaskApp_Web.Services.IServices
{
    public interface IBadgeService
    {
        Task<IEnumerable<UserBadge>> GetUserBadgesAsync(int userId);
        Task<IEnumerable<Badge>> GetAvailableBadgesAsync(int userId);
        Task GiveBadgeIfEligible(int userId, int completedTaskCount);
    }
}
