﻿using TaskApp_Web.Models;
using TaskApp_Web.Repositories.IRepositories;
using TaskApp_Web.Services.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskApp_Web.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;
        private readonly IUserBadgeRepository _userBadgeRepository;

        public BadgeService(IBadgeRepository badgeRepository, IUserBadgeRepository userBadgeRepository)
        {
            _badgeRepository = badgeRepository;
            _userBadgeRepository = userBadgeRepository;
        }

        public async Task<IEnumerable<Badge>> GetAvailableBadgesAsync(int userId)
        {
            return await _badgeRepository.GetAvailableBadgesAsync(userId);
        }

        public async Task<IEnumerable<UserBadge>> GetUserBadgesAsync(int userId)
        {
            return await _userBadgeRepository.GetUserBadgesAsync(userId);
        }

        public async Task<IEnumerable<Badge>> GetBadgesByTaskCompletionCountAsync(int taskCompletionCount)
        {
            return await _badgeRepository.GetBadgesByTaskCompletionCountAsync(taskCompletionCount);
        }

        public Task GiveBadgeIfEligible(int userId, int completedTaskCount)
        {
            throw new NotImplementedException();
        }
    }
}