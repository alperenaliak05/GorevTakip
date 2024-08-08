using DepartmanDTO.models;
using Models;
using Models.TaskAppDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USERDTO.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DepartmentDTO Department { get; set; }
        public List<TaskDTO> Tasks { get; set; }
    }
}
