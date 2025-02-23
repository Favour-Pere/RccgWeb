using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;

namespace RccgWeb.Controllers
{
    public class ProgramActivityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgramActivityController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //[HttpGet]
        //public IActionResult AddActivity()
        //{
        //    return View(new ProgramActivityViewModel());
        //}

        [HttpPost]
        public async Task<IActionResult> AddActivity([FromBody] ProgramActivity programActivity)
        {
            if (programActivity == null || string.IsNullOrEmpty(programActivity.ChurchId))
            {
                return BadRequest(new { message = "Invalid activity data. ChurchId is required." });
            }

            try
            {
                // Check for existing Zone, Area, or Parish with the given ChurchId
                var existingChurch = await _context.Zones.AsNoTracking().FirstOrDefaultAsync(z => z.ChurchId == programActivity.ChurchId)
                                  ?? (object)await _context.Areas.AsNoTracking().FirstOrDefaultAsync(a => a.ChurchId == programActivity.ChurchId)
                                  ?? (object)await _context.Parishes.AsNoTracking().FirstOrDefaultAsync(p => p.ChurchId == programActivity.ChurchId);

                if (existingChurch == null)
                {
                    return NotFound(new { message = "No Zone, Area, or Parish found for the provided ChurchId." });
                }

                // Ensure DateCreated is properly set
                if (programActivity.DateCreated == default)
                {
                    programActivity.DateCreated = DateTime.Now;
                }

                _context.ProgramActivities.Add(programActivity);
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
                    message = "An error occurred while adding the activity.",
                    error = ex.Message
                });
            }
        }
    }
}