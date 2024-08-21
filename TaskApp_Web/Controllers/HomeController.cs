using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskApp_Web.Models;
using System.Diagnostics;
using TaskApp_Web.Models.DTO;

namespace TaskApp_Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
