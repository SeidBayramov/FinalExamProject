using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalExamProject.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

    }
}