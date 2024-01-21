using AutoMapper;
using FinalExamProject.Areas.Manage.ViewModel.Fruit;
using FinalExamProject.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace FinalExamProject.Areas.Manage.Controllers
{
    public class FruitController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _map;

        public FruitController(AppDbContext context, IWebHostEnvironment env, IMapper map)
        {
            _context = context;
            _env = env;
            _map = map;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FruitCreateVm vm)
        {

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int Id)
        {

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Update(FruitUpdateVm vm)
        {

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {

            return RedirectToAction(nameof(Index));
        }

    }
}
