using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RccgWeb.Data;
using RccgWeb.Models;

namespace RccgWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AreaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateArea()
        {
            var existingZone = _context.Zones.FirstOrDefault();

            if (existingZone == null)
            {
                return BadRequest("No existing Zone found. Please create an Area first.");
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
    }
}