using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RccgWeb.Data;
using RccgWeb.Models;

namespace RccgWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParishController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParishController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateParish()
        {
            var exisitingArea = _context.Areas.FirstOrDefault();

            if (exisitingArea == null)
            {
                return BadRequest("No exisiting Area found. Please create an Area first.");
            }

            var newParish = new Parish
            {
                ParishId = Guid.NewGuid(),
                ChurchId = ChurchIdGenerator.GenerateChurchId(_context.Parishes),
                ParishName = "Test Parish",
                ParishPastor = "Pastor Parish",
                DateCreated = DateTime.Now,
                Location = "Parish Location",
                AreaId = exisitingArea.AreaId
            };

            _context.Parishes.Add(newParish);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Parish added successfully!",
                data = newParish
            });
        }
    }
}