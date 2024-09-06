using DTOs.TaskApp_WebDTO;

namespace Models.USERDTO
{
    public class UserWithTasksDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TaskDTO> Tasks { get; set; }
    }

}
