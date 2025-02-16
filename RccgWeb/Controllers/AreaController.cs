using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.Services.Interfaces;
using RccgWeb.ViewModel;

namespace RccgWeb.Controllers
{
    public class AreaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAreaService _areaService;

        public AreaController(ApplicationDbContext context, IAreaService areaService)
        {
            _context = context;
            _areaService = areaService;
        }

        public async Task<IActionResult> Index()
        {
            var areas = await _context.Areas.ToListAsync();

            return View(areas);
        }

        [HttpGet]
        public async Task<IActionResult> AddArea()
        {
            var zones = await _context.Zones.Select(z => new SelectListItem
            {
                Value = z.ZoneId.ToString(),
                Text = z.ZoneName
            }).ToListAsync();

            var viewModel = new AreaViewModel
            {
                Zones = zones
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddArea(AreaViewModel areaViewModel)
        {
            if (ModelState.IsValid)
            {
                var area = new Area
                {
                    AreaId = Guid.NewGuid(),
                    ChurchId = ChurchIdGenerator.GenerateChurchId(_context),
                    AreaName = areaViewModel.AreaName,
                    AreaPastor = areaViewModel.AreaPastor,
                    DateCreated = DateTime.Now,
                    Location = areaViewModel.Location,
                    ZoneId = areaViewModel.ZoneId
                };
                _context.Areas.Add(area);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            areaViewModel.Zones = await _context.Zones
        .Select(z => new SelectListItem { Value = z.ZoneId.ToString(), Text = z.ZoneName })
        .ToListAsync();
            return View(areaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var area = await _areaService.GetAreaByIdAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        public async Task<IActionResult> ParishesUnderArea(Guid id)
        {
            var parishes = await _context.Parishes.Where(p => p.ParishId == id).ToListAsync();

            ViewBag.ParishId = id;

            return View(parishes);
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateArea()
        //{
        //    var existingZone = _context.Zones.FirstOrDefault();

        //    if (existingZone == null)
        //    {
        //        return BadRequest("No existing Zone found. Please create a Zone first.");
        //    }

        //    var newArea = new Area
        //    {
        //        AreaId = Guid.NewGuid(),
        //        ChurchId = ChurchIdGenerator.GenerateChurchId(_context),
        //        AreaName = "Test Area",
        //        AreaPastor = "Test Pastor",
        //        DateCreated = DateTime.Now,
        //        Location = "Area Location",
        //        ZoneId = existingZone.ZoneId
        //    };

        //    _context.Areas.Add(newArea);

        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        message = "Area added successfully!",
        //        data = newArea
        //    });
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAreas()
        //{
        //    var areas = await _areaService.GetAreasAsync();

        //    return Ok(areas);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAreaById(Guid id)
        //{
        //    var area = await _areaService.GetAreaByIdAsync(id);

        //    if (area == null)
        //    {
        //        return NotFound(new
        //        {
        //            message = "Area not found"
        //        });
        //    }

        //    return Ok(area);
        //}
    }
}