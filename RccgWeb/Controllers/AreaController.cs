using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.Services.Interfaces;
using RccgWeb.ViewModel;

namespace RccgWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        public IActionResult AddArea()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddArea(AreaViewModel areaViewModel)
        {
            if (area.ZoneId != null)
            {
                area.AreaId = Guid.NewGuid();
                area.ChurchId = ChurchIdGenerator.GenerateChurchId(_context);

                await _areaService.AddAreaAsync(area);
            }
            else
            {
                return BadRequest("No existing Zone found. Please create a Zone first.");
            }
            return Ok(new
            {
                message = "Area added successfully"
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateArea()
        {
            var existingZone = _context.Zones.FirstOrDefault();

            if (existingZone == null)
            {
                return BadRequest("No existing Zone found. Please create a Zone first.");
            }

            var newArea = new Area
            {
                AreaId = Guid.NewGuid(),
                ChurchId = ChurchIdGenerator.GenerateChurchId(_context),
                AreaName = "Test Area",
                AreaPastor = "Test Pastor",
                DateCreated = DateTime.Now,
                Location = "Area Location",
                ZoneId = existingZone.ZoneId
            };

            _context.Areas.Add(newArea);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Area added successfully!",
                data = newArea
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await _areaService.GetAreasAsync();

            return Ok(areas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAreaById(Guid id)
        {
            var area = await _areaService.GetAreaByIdAsync(id);

            if (area == null)
            {
                return NotFound(new
                {
                    message = "Area not found"
                });
            }

            return Ok(area);
        }
    }
}