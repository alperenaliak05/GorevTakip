namespace Models
{
    public class UserBadge
    {
        public int UserBadgeId { get; set; }
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime EarnedDate { get; set; }
        public Users User { get; set; }
        public Badge Badge { get; set; }
    }
}
