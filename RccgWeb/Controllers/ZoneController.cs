using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.ViewModel;

namespace RccgWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ZoneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var zones = await _context.Zones.ToListAsync();

            return View(zones);
        }

        [HttpGet]
        public IActionResult AddZone()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddZone(ZoneViewModel zoneViewModel)
        {
            if (ModelState.IsValid)
            {
                var zone = new Zone
                {
                    ZoneId = Guid.NewGuid(),
                    ChurchId = ChurchIdGenerator.GenerateChurchId(_context),
                    ZoneName = zoneViewModel.ZoneName,
                    ZonePastor = zoneViewModel.ZonePastor,
                    DateCreated = DateTime.Now,
                    Location = zoneViewModel.Location,
                };

                _context.Zones.Add(zone);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(zoneViewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var zone = await _context.Zones.FirstOrDefaultAsync(z => z.ZoneId == id);

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        public async Task<IActionResult> AreasUnderZone(Guid id)
        {
            var areas = await _context.Areas.Where(a => a.ZoneId == id).ToListAsync();

            ViewBag.ZoneId = id;

            return View(areas);
        }
    }
}