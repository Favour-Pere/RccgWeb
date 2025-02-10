using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RccgWeb.Data;
using RccgWeb.Models;

namespace RccgWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateZone()
        {
            var newZone = new Zone
            {
                ZoneId = Guid.NewGuid(),
                ChurchId = ChurchIdGenerator.GenerateChurchId(_context.Zones),
                ZoneName = "Auto Zone",
                ZonePastor = "Pastor Auto",
                DateCreated = DateTime.Now,
                Location = "Auto Location"
            };

            _context.Zones.Add(newZone);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Zone added successfully!",
                data = newZone
            });
        }
    }
}