using DTOs.TaskApp_WebDTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.USERDTO;
using Repositories.IReporsitory;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllWithTasksAndDepartment();

            var userDtos = users.Select(user => new UserDTO
            {
                Id = ((Users)user).Id,  
                FirstName = ((Users)user).FirstName,
                LastName = ((Users)user).LastName,
                Email = ((Users)user).Email,
                Department = new DepartmentDTO
                {
                    Id = ((Users)user).Department.Id,
                    Name = ((Users)user).Department.Name
                },
                Tasks = ((Users)user).Tasks?.Select(task => new TaskDTO
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    AssignedToUserId = task.AssignedToUserId,
                    AssignedByUserId = task.AssignedByUserId,
                    DueDate = task.DueDate,
                    Status = task.Status
                }).ToList()
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUserWithTasksAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = new DepartmentDTO
                {
                    Id = user.Department.Id,
                    Name = user.Department.Name
                },
                Tasks = user.Tasks?.Select(task => new TaskDTO
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    AssignedToUserId = task.AssignedToUserId,
                    AssignedByUserId = task.AssignedByUserId,
                    DueDate = task.DueDate,
                    Status = task.Status
                }).ToList()
            };

            return Ok(userDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreatedDTO userCreatedDTO)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            if (currentUser.Department.Name != "İnsan Kaynakları Uzmanı")
            {
                return Forbid("Bu işlemi sadece İnsan Kaynakları Uzmanı gerçekleştirebilir.");
            }

            var user = new Users
            {
                FirstName = userCreatedDTO.FirstName,
                LastName = userCreatedDTO.LastName,
                Email = userCreatedDTO.Email,
                DepartmentId = userCreatedDTO.DepartmentId
            };

            await _userRepository.AddAsync(user);

            var userDto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = new DepartmentDTO
                {
                    Id = user.Department.Id,
                    Name = user.Department.Name
                }
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
        }


        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.DepartmentId = userDto.Department.Id;

            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
