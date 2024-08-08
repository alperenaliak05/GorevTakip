using Microsoft.AspNetCore.Mvc;
using TaskApp_Web.Repositories;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, bool rememberMe)
    {
        var user = _userRepository.GetUserByEmailAsync(email);
        if (user != null)
        {
           
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = "Invalid credentials";
        return View();
    }
}
