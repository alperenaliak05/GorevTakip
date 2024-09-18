namespace Models
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int TaskCompletionCount { get; set; }
        public bool IsSpecialBadge { get; set; } = false;
        public int TaskCompletionThreshold { get; set; }
    }
}
