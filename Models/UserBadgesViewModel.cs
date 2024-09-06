namespace Models
{
    public class UserBadgesViewModel
    {
        public IEnumerable<UserBadge> UserBadges { get; set; }
        public IEnumerable<Badge> AvailableBadges { get; set; }
    }
}
