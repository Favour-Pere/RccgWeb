using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;

namespace RccgWeb.Controllers
{
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

        public IActionResult AddZone()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddZone(Zone zone)
        {
            if (ModelState.IsValid)
            {
                zone.ZoneId = Guid.NewGuid();
                zone.ChurchId = ChurchIdGenerator.GenerateChurchId(_context);
                zone.DateCreated = DateTime.Now;

                _context.Zones.Add(zone);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(zone);
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

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateZone()
        //{
        //    var newZone = new Zone
        //    {
        //        ZoneId = Guid.NewGuid(),
        //        ChurchId = ChurchIdGenerator.GenerateChurchId(_context),
        //        ZoneName = "Auto Zone",
        //        ZonePastor = "Pastor Auto",
        //        DateCreated = DateTime.Now,
        //        Location = "Auto Location"
        //    };

        //    _context.Zones.Add(newZone);

        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        message = "Zone added successfully!",
        //        data = newZone
        //    });
        //}
    }
}