namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public ICollection<ToTask> Tasks { get; set; }
        public ICollection<ToTask> AssignedTasks { get; set; }
    }
}
