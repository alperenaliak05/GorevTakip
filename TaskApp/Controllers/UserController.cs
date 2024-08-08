using Microsoft.AspNetCore.Mvc;
using Models;  // User ve Task gibi modellerin bulunduğu namespace
using Repositories.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using USERDTO.Models;
using DepartmanDTO.models;
using Models.USERDTO;
using Microsoft.EntityFrameworkCore;
using Models.TaskAppDTO;

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
                Id = ((User)user).Id,  // user nesnesinin doğru tipe dönüştürüldüğünden emin olun
                FirstName = ((User)user).FirstName,
                LastName = ((User)user).LastName,
                Email = ((User)user).Email,
                Department = new DepartmentDTO
                {
                    Id = ((User)user).Department.Id,
                    Name = ((User)user).Department.Name
                },
                Tasks = ((User)user).Tasks?.Select(task => new TaskDTO
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
            var user = new User
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
