// UserController.cs
using Microsoft.AspNetCore.Mvc;
using TaskApp_Web.Repositories;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult AllUsers()
    {
        var users = _userRepository.GetAllUsers();
        return View(users);
    }

    public IActionResult UserProfile(int id)
    {
        var user = _userRepository.GetUserByIdAsync(id);
        return View(user);
    }
}
