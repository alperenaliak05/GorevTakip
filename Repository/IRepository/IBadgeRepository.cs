using Models;

namespace Repositories.IReporsitory
{
    public interface IBadgeRepository
    {
        Task<IEnumerable<Badge>> GetAllBadgesAsync();
        Task<Badge> GetBadgeByIdAsync(int badgeId);
        Task<bool> AddBadgeAsync(Badge badge);
        Task<bool> UpdateBadgeAsync(Badge badge);
        Task<bool> DeleteBadgeAsync(int badgeId);
        Task<IEnumerable<Badge>> GetBadgesByTaskCompletionCountAsync(int taskCompletionCount);
        Task<IEnumerable<Badge>> GetAvailableBadgesAsync(int userId);
    }
}
