using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RccgWeb.Data;
using RccgWeb.Models;

namespace RccgWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramActivityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProgramActivityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddActivity")]
        public async Task<IActionResult> AddActivity([FromBody] ProgramActivity programActivity)
        {
            if (programActivity == null)
            {
                return BadRequest(new
                {
                    message = "Invalid activity data"
                });
            }

            try
            {
                int idCount = new[]
                {
                    programActivity.ZoneId,
                    programActivity.AreaId,
                    programActivity.ParishId
                }.Count(id => id.HasValue);

                if (idCount != 1)
                {
                    return BadRequest(new
                    {
                        message = "Program Activity must be linked to one of zone, Area, or parish."
                    });
                }

                if (programActivity.ZoneId.HasValue)
                {
                    programActivity.ChurchId = _context.Zones.Where
                }
            }
        }
    }
}