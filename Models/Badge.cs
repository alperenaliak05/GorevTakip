namespace Models
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // Rozetin görsel URL'si
        public int TaskCompletionCount { get; set; } // Görev tamamlama sayısına göre rozet verilmesi için
        public bool IsSpecialBadge { get; set; } = false;
        public int TaskCompletionThreshold { get; set; }
    }
}
