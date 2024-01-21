using AutoMapper;
using FinalExamProject.Areas.Manage.ViewModel.Fruit;
using FinalExamProject.DAL.Context;
using FinalExamProject.Helpers;
using FinalExamProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExamProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]
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
            var fruit = await _context.Fruits.ToListAsync();
            return View(fruit);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FruitCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (!vm.Image.CheckImage())
            {
                ModelState.AddModelError("", "Image type or length is wrong");
                return View(vm);
            }
            
            var fruit= _map.Map<Fruit>(vm);
            fruit.ImageUrl = vm.Image.Upload(_env.WebRootPath, "Upload");

            await _context.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int Id)
        {
            if(Id <= 0)
            {
                ModelState.AddModelError("", "Bele bir id yoxdur");
                return View();
            }
            var fruit = await _context.Fruits.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (fruit == null)
            {
                ModelState.AddModelError("", "Bele bir id yoxdur");
                return View();
            }
            FruitUpdateVm updateVm = new FruitUpdateVm()
            {
                Id = fruit.Id,
                Title = fruit.Title,
                SubTitle = fruit.SubTitle,
                ImageUrl = fruit.ImageUrl
            };

            return View(updateVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(FruitUpdateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existfruit = await _context.Fruits.Where(x => x.Id == vm.Id).FirstOrDefaultAsync();

            if (existfruit == null)
            {
                ModelState.AddModelError("", "Sehvlik bas verdi yeniden yoxla");
                return View(vm);
            }
            if (vm.Image != null)
            {
                if (!vm.Image.CheckImage())
                {
                    ModelState.AddModelError("", "Image type or length is wrong");
                    return View(vm);
                }
                existfruit.Title = vm.Title;
                existfruit.SubTitle = vm.SubTitle;
                existfruit.ImageUrl = vm.Image.Upload(_env.WebRootPath, "Upload");
                await _context.SaveChangesAsync();
            }
            existfruit.Title = vm.Title;
            existfruit.SubTitle = vm.SubTitle;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (Id <= 0)
            {
                ModelState.AddModelError("", "Bele bir id yoxdur");
                return View();
            }
            var fruit = await _context.Fruits.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (fruit == null)
            {
                ModelState.AddModelError("", "Bele bir id yoxdur");
                return View();
            }
            _context.Remove(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
