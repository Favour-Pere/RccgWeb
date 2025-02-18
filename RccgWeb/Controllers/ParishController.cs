using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.ViewModel;

namespace RccgWeb.Controllers
{
    public class ParishController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParishController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var parishes = await _context.Parishes.ToListAsync();

            return View(parishes);
        }

        public async Task<IActionResult> AddParish()
        {
            var areas = await _context.Areas.Select(a => new SelectListItem
            {
                Value = a.AreaId.ToString(),
                Text = a.AreaName
            }).ToListAsync();

            var model = new ParishViewModel
            {
                Areas = areas
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddParish(ParishViewModel parishViewModel)
        {
            if (ModelState.IsValid)
            {
                var parish = new Parish
                {
                    ParishId = Guid.NewGuid(),
                    ChurchId = ChurchIdGenerator.GenerateChurchId(_context),
                    ParishName = parishViewModel.ParishName,
                    ParishPastor = parishViewModel.ParishPastor,
                    DateCreated = DateTime.Now,
                    Location = parishViewModel.Location,
                    AreaId = parishViewModel.AreaId
                };

                await _context.Parishes.AddAsync(parish);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            parishViewModel.Areas = await _context.Areas.Select(a => new SelectListItem
            {
                Value = a.AreaId.ToString(),
                Text = a.AreaName
            }).ToListAsync();

            return View(parishViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var parish = await _context.Parishes.FindAsync(id);

            if (parish == null)
            {
                return NotFound();
            }

            return View(parish);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateParish()
        {
            var exisitingArea = _context.Areas.FirstOrDefault();

            if (exisitingArea == null)
            {
                return BadRequest("No exisiting Area found. Please create an Area first.");
            }

            var newParish = new Parish
            {
                ParishId = Guid.NewGuid(),
                ChurchId = ChurchIdGenerator.GenerateChurchId(_context),
                ParishName = "Test Parish",
                ParishPastor = "Pastor Parish",
                DateCreated = DateTime.Now,
                Location = "Parish Location",
                AreaId = exisitingArea.AreaId
            };

            _context.Parishes.Add(newParish);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Parish added successfully!",
                data = newParish
            });
        }
    }
}