using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.Services.Interfaces;

namespace RccgWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAreaService _areaService;

        public AreaController(ApplicationDbContext context, IAreaService areaService)
        {
            _context = context;
            _areaService = areaService;
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

        [HttpPost]
        public async Task<IActionResult> AddArea([FromBody] Area area)
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
    }
}