namespace Models
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? DepartmentName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WorkingHours { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public UsersStatus Status { get; set; }
        public IEnumerable<Badge> UserBadges { get; set; }
    }
}
