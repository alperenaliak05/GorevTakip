using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskAppWeb.Models;
using System.Diagnostics;
using TaskAppWeb.Models.DTO;

namespace TaskAppWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
