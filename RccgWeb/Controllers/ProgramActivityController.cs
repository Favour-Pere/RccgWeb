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
                    var zone = await _context.Zones.FindAsync(programActivity.ZoneId);
                    programActivity.ChurchId = zone?.ChurchId;

                    //programActivity.ChurchId = _context.Zones.Where(z => z.ZoneId == programActivity.ZoneId).Select(z => z.ChurchId).FirstOrDefault();
                }
                else if (programActivity.AreaId.HasValue)
                {
                    var area = await _context.Areas.FindAsync(programActivity.AreaId);
                    programActivity.ChurchId = area?.ChurchId;
                    //programActivity.ChurchId = _context.Areas.Where(a => a.AreaId == programActivity.AreaId).Select(a => a.ChurchId).FirstOrDefault();
                }
                else if (programActivity.ParishId.HasValue)
                {

                    var parish = await _context.Parishes.FindAsync(programActivity.ParishId);
                    programActivity.ChurchId = parish?.ChurchId;
                    //programActivity.ChurchId = _context.Parishes.Where(p => p.ParishId == programActivity.ParishId).Select(p => p.ChurchId).FirstOrDefault();
                }

                if (string.IsNullOrEmpty(programActivity.ChurchId))
                {
                    return BadRequest(new
                    {
                        message = "Invalid ZoneId, AreaId, or ParishId. ChurchId could not be determined."
                    });
                }

                if (programActivity.DateCreated == default)
                {
                    programActivity.DateCreated = DateTime.Now;
                }

                _context.Activities.Add(programActivity);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Program activity added successfully",
                    data = programActivity
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occured while adding the activity",

                    error = ex.Message.ToString()
                });
            }
        }
    }
}