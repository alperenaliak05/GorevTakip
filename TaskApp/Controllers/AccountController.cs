using Microsoft.AspNetCore.Mvc;
using Repositories.IReporsitory;


[ApiController]
[Route("[controller]")]

public class AccountController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


}


