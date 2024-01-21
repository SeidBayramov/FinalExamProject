using FinalExamProject.Helpers;
using FinalExamProject.Models;
using FinalExamProject.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalExamProject.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginmaneger;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> siginmaneger, RoleManager<IdentityRole> rolemanager)
        {
            _userManager = userManager;
            _siginmaneger = siginmaneger;
            _rolemanager = rolemanager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm) 
        {
            RegisterVmValidation validationRules = new RegisterVmValidation();
            var respons= validationRules.Validate(vm);
            if (!respons.IsValid)
            {
                foreach (var eror in respons.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(eror.PropertyName,eror.ErrorMessage);
                }
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser user = new AppUser()
            {
                Email = vm.Email,
                Surname = vm.Surname,
                Name = vm.Name,
                UserName = vm.Username
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.Clear();

                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
            await _siginmaneger.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm,string? returnurl)
        {
            LoginVmValidation validationRules = new LoginVmValidation();
            var respons = validationRules.Validate(vm);
            if (!respons.IsValid)
            {
                foreach (var eror in respons.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(eror.PropertyName, eror.ErrorMessage);
                }
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(vm.UsernameorEmail) ?? await _userManager.FindByNameAsync(vm.UsernameorEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or password is wrong");
                return View(vm);
            }
            var result = await _siginmaneger.PasswordSignInAsync(user, vm.Password, false, true);
            if (!result.Succeeded)
            {

                if (result.IsLockedOut)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "Your account is banned for a few minutes");
                    return View(vm);
                }

                ModelState.AddModelError("", "Username/Email or password is wrong");
                return View(vm);
            }

            if (returnurl != null&&returnurl.Contains("Login"))
            {
                return Redirect(returnurl);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _siginmaneger.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Createrole()
        {
            foreach (var role in Enum.GetNames(typeof(UserRole)))
            {
                if(!await _rolemanager.RoleExistsAsync(role))
                {
                    await _rolemanager.CreateAsync(new IdentityRole(role));
                }
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
