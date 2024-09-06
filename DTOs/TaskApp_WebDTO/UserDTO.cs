
namespace DTOs.TaskApp_WebDTO;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DepartmentDTO Department { get; set; }
    public List<TaskDTO> Tasks { get; set; }
}
