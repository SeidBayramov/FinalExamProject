using FinalExamProject.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FinalExamProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Register(RegisterVm vm) 
        {

            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> LogOut()
        {
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Createrole()
        {

            return RedirectToAction("Index");

        }


    }
}
