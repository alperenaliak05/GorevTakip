using TaskAppWeb.Models;

namespace TaskAppWeb.Repositories.IRepositories
{
    public interface IUserToDoListRepository
    {
        Task<IEnumerable<UserToDoList>> GetToDoListsByUserIdAsync(int userId);
        Task<bool> AddToDoListAsync(UserToDoList toDoList);
        Task<bool> UpdateToDoListAsync(UserToDoList toDoList);
        Task<bool> DeleteToDoListAsync(int id);
    }

}
