using Models;

namespace TaskApp_Web.Models
{
    public class UserBadge
    {
        public int UserBadgeId { get; set; }
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime EarnedDate { get; set; }

        // Navigation Properties
        public Users User { get; set; }
        public Badge Badge { get; set; }
    }
}
